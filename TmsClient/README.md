# Training Management System (TMS) - Client Library

## Module 2 Lab Session 2: TypeScript Type System

A professional TypeScript implementation demonstrating discriminated unions, generics, and the Temporal API for modern date/time handling.

## Project Overview

This project implements the client-side type system for a Training Management System using advanced TypeScript features. It demonstrates type-safe data modeling, generic programming, and modern date/time handling with the Temporal API.

## What Problem Does This Solve?

Legacy JavaScript applications suffer from common type safety and date/time issues:

- **Type Confusion**: Runtime errors from accessing wrong properties on union types
- **Impossible States**: Boolean flags creating 27 invalid state combinations
- **Code Duplication**: Repetitive API response handling for different data types
- **Date/Time Bugs**: Timezone confusion and mutable Date objects causing data corruption

This implementation addresses these issues using modern TypeScript features.

## Exercise 4: Assessment Types (Discriminated Unions)

**Problem**: Different assessment types (quizzes, lab assignments) need different grade calculations. Legacy code used type checking and casting.

**Solution**: Discriminated unions with type narrowing

```typescript
export interface Quiz {
    readonly id: string;
    kind: "quiz";              // Discriminant field
    title: string;
    correctAnswers: number;
    totalQuestions: number;
}

export interface LabAssignment {
    readonly id: string;
    kind: "lab";               // Discriminant field
    title: string;
    functionalityScore: number;
    codeQualityScore: number;
}

export type AssessmentItem = Quiz | LabAssignment;

export function calculateGrade(item: AssessmentItem): number {
    switch (item.kind) {
        case "quiz":
            // TypeScript knows item is Quiz here
            return Math.round((item.correctAnswers / item.totalQuestions) * 100);
        case "lab":
            // TypeScript knows item is LabAssignment here
            return Math.round(
                item.functionalityScore * 0.7 + item.codeQualityScore * 0.3,
            );
    }
}
```

**Key Takeaways**:
- Discriminant field (`kind`) enables type narrowing
- Compiler prevents accessing wrong properties
- Exhaustive checking ensures all cases are handled
- Works with JSON data (no classes needed)

## Exercise 5: Enrollment Lifecycle (State Machine Union)

**Problem**: Boolean flags create impossible states. Can `isPending` and `isDropped` both be true?

**Solution**: State machine with discriminated unions

```typescript
export type EnrollmentStatus =
    | { status: "PENDING"; requestedAt: Temporal.Instant; studentId: string; courseId: string }
    | { status: "APPROVED"; approvedBy: string; approvedAt: Temporal.Instant }
    | { status: "ACTIVE"; startDate: Temporal.PlainDate; currentGrade?: number }
    | { status: "COMPLETED"; finalGrade: number; completedAt: Temporal.Instant }
    | { status: "DROPPED"; reason: string; droppedAt: Temporal.Instant };

export function describeEnrollment(enrollment: EnrollmentStatus): string {
    switch (enrollment.status) {
        case "PENDING":
            return `Awaiting approval since ${enrollment.requestedAt}`;
        case "APPROVED":
            return `Approved by ${enrollment.approvedBy}`;
        case "ACTIVE":
            return enrollment.currentGrade !== undefined
                ? `In progress grade so far: ${enrollment.currentGrade}`
                : `In progress not yet graded`;
        case "COMPLETED":
            return `Finished with ${enrollment.finalGrade}`;
        case "DROPPED":
            return `Dropped: ${enrollment.reason}`;
        default: {
            const _check: never = enrollment;
            throw new Error(`Unhandled status: ${JSON.stringify(_check)}`);
        }
    }
}
```

**Key Takeaways**:
- Only 5 valid states (not 27 impossible combinations)
- Each state carries only relevant data
- `never` type ensures exhaustive checking
- Compiler error if you forget to handle a case

## Exercise 6: Reusable API Response (Generics)

**Problem**: Repetitive API response types for different data types (Student, Course, etc.)

**Solution**: Generic type with type parameter

```typescript
export type ApiResponse<T> =
    | { status: "loading" }
    | { status: "success"; data: T; fetchedAt: Temporal.Instant }
    | { status: "error"; message: string; statusCode: number };

export function renderResponse<T>(
    response: ApiResponse<T>,
    formatter: (data: T) => string,
): string {
    switch (response.status) {
        case "loading":
            return "Loading...";
        case "success":
            return formatter(response.data);
        case "error":
            return `Error ${response.statusCode}: ${response.message}`;
    }
}

// Usage with Student
const studentRes: ApiResponse<Student> = {
    status: "success",
    data: { id: "STU-001", name: "Dawit Bekele", gpa: 3.4 },
    fetchedAt: Temporal.Now.instant(),
};

// Usage with Course array
const courseListRes: ApiResponse<Course[]> = {
    status: "success",
    data: [{ id: "CRS-101", title: "Web Development" }],
    fetchedAt: Temporal.Now.instant(),
};
```

**Key Takeaways**:
- One type definition works for all data types
- Type safety preserved (TypeScript knows `T` in each usage)
- Formatter pattern lets caller decide how to display data
- DRY principle (Don't Repeat Yourself)

## Exercise 7: Temporal Timestamps

**Problem**: JavaScript `Date` object is mutable, timezone-confused, and inconsistent.

**Solution**: Temporal API with immutable, timezone-aware types

```typescript
// 1. Record exact moment in UTC
const approvedAt = Temporal.Now.instant();
console.log(`Approved at (UTC): ${approvedAt}`);

// 2. Display in local timezones
const addisTime = approvedAt.toZonedDateTimeISO("Africa/Addis_Ababa");
const londonTime = approvedAt.toZonedDateTimeISO("Europe/London");
console.log(`Addis: ${addisTime.toPlainTime()}`);
console.log(`London: ${londonTime.toPlainTime()}`);

// 3. Date-only calculations (no time)
const courseStart = Temporal.PlainDate.from("2026-09-01");
const today = Temporal.Now.plainDateISO();
const daysUntilStart = today.until(courseStart).total({ unit: "days" });
console.log(`${Math.floor(daysUntilStart)} days until course starts`);

// 4. Duration calculations
const deadline = Temporal.PlainDate.from("2026-12-15");
const remaining = today.until(deadline);
console.log(`${remaining.total({ unit: "days" })} days until assignment is due`);
```

**Key Takeaways**:
- `Temporal.Instant`: Exact moment in UTC (for recording events)
- `Temporal.PlainDate`: Date without time (for course start dates)
- `Temporal.ZonedDateTime`: Display in user's timezone
- Immutable (cannot be changed accidentally)
- Nanosecond precision

## Technical Architecture

### Domain Models (`models/`)

**assessment.model.ts**:
- `Quiz`: Simple percentage calculation
- `LabAssignment`: Weighted score (70% functionality, 30% code quality)
- `AssessmentItem`: Discriminated union
- `calculateGrade()`: Type-safe grade calculator

**enrollment.model.ts**:
- `EnrollmentStatus`: State machine with 5 states
- `describeEnrollment()`: State handler with exhaustive checking

**api-response.model.ts**:
- `ApiResponse<T>`: Generic response wrapper
- `renderResponse<T>()`: Generic formatter function

**student.model.ts**:
- `Student`: Student data with Temporal timestamps

**course.model.ts**:
- `Course`: Course data with Temporal dates

### Application Entry Point (`index.ts`)

Demonstrates all exercises with comprehensive examples and test cases.

## Key Concepts Demonstrated

### 1. Discriminated Unions
- Common discriminant field with literal types
- Type narrowing in switch statements
- Exhaustive checking with `never` type

### 2. Generics
- Type parameters (`<T>`)
- Reusable code for multiple types
- Type safety preserved

### 3. Temporal API
- `Instant` for UTC timestamps
- `PlainDate` for dates without time
- `ZonedDateTime` for timezone display
- Duration calculations

### 4. Type Safety
- Readonly properties
- Literal types for discriminants
- Exhaustive pattern matching

## Running the Project

```bash
# Install dependencies
npm install

# Compile TypeScript
npx tsc

# Run the application
node index.js
```

## Expected Output

```
Quiz grade: 80%
Lab grade: 87%
Awaiting approval since 2026-05-31T08:46:25.977185891Z
Dawit Bekele GPA: 3.4
Web Development Fundamentals
Approved at (UTC): 2026-05-31T08:46:25.991185991Z
Addis: 11:46:25.991185991
London: 09:46:25.991185991
93 days until course starts
198 days until assignment is due
```

## Main Takeaways

1. **Discriminated Unions**: Make impossible states unrepresentable
2. **Generics**: Write reusable code that works with any type
3. **Temporal API**: Modern, timezone-aware date/time handling
4. **Type Safety**: Compiler catches errors before runtime
5. **Exhaustive Checking**: Ensure all cases are handled

## Technology Stack

- **TypeScript 6.0**: Modern type system features
- **Temporal API**: Modern date/time handling (polyfill)
- **Node.js**: Runtime environment
- **Discriminated Unions**: Type-safe variant types
- **Generics**: Reusable type-safe code

## Project Structure

```
TmsClient/
├── index.ts                    # Main application
├── models/
│   ├── assessment.model.ts     # Quiz and LabAssignment types
│   ├── enrollment.model.ts     # State machine union
│   ├── api-response.model.ts   # Generic response wrapper
│   ├── student.model.ts        # Student data model
│   └── course.model.ts         # Course data model
├── package.json                # Dependencies
├── tsconfig.json               # TypeScript configuration
└── README.md                   # This file
```

## License

Educational project for TypeScript Essentials Module 2 Lab Session 2.
