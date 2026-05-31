# Training Management System (TMS) - Core Logic Engine

## Sessions 1 & 2: Data Model Foundation and Query Classification

A professional implementation of C# 14 Essentials Module 1 Lab Sessions 1 and 2, demonstrating modern C# features for building robust, type-safe data models with business logic and analytics.

## Project Overview

This project implements the core domain models, business logic, and analytics for a Training Management System. 

**Session 1** establishes a solid foundation with null safety, financial precision, immutable records, and validated properties.

**Session 2** builds on this foundation by adding defensive programming with guard clauses, academic standing classification with pattern matching, and data analytics with LINQ.

## What Problem Does This Solve?

Legacy training management systems suffer from common data integrity issues:

- **Null Reference Exceptions**: Crashes when student data is incomplete
- **Financial Precision Errors**: Grant calculations drift due to floating-point arithmetic
- **Data Corruption**: Enrollment records accidentally modified after creation
- **Invalid States**: Courses with negative capacity or empty titles
- **Type Confusion**: Different assessment types require different grade calculations
- **Unsafe Registration**: No validation when enrolling students in courses
- **Poor Analytics**: Difficult to query and analyze student performance data

This implementation addresses these issues using modern C# features.

## Session 1: Data Model Foundation

### Exercise 1: Null Safety Patterns

**Problem**: Student registration data is often incomplete. The legacy system crashes with NullReferenceException when accessing missing fields.

**Solution**: C# nullable reference types with null-coalescing operators

```csharp
string? region = null;
string? upperRegion = region?.ToUpper();           // Null-conditional (?.)
string displayRegion = region ?? "Unassigned";     // Null-coalescing (??)
region ??= "Addis Ababa";                          // Null-coalescing assignment (??=)
```

**Key Takeaways**:
- `?.` safely calls methods on potentially null objects
- `??` provides default values for null cases
- `??=` assigns only when the variable is null
- Prevents NullReferenceException crashes

### Exercise 2: Financial Precision

**Problem**: The legacy system uses `double` for grant calculations, causing precision drift. A grant of $1,999.99 per student × 100,000 students produces incorrect totals.

**Solution**: Use `decimal` type for all financial calculations

```csharp
// WRONG: double has floating-point errors
double legacyGrant = 1999.99;
double legacyTotal = legacyGrant * 100_000;  // Precision drift

// CORRECT: decimal has exact precision
decimal grant = 1999.99m;
decimal total = grant * 100_000m;  // Exact calculation
```

**Key Takeaways**:
- `double` uses binary floating-point (0.1 + 0.2 ≠ 0.3)
- `decimal` uses base-10 arithmetic (exact for money)
- Always use `decimal` for financial calculations
- The `m` suffix indicates a decimal literal

### Exercise 3: Data Integrity with Immutable Records

**Problem**: Enrollment records were accidentally modified after creation, corrupting historical data.

**Solution**: C# records provide immutability and value-based equality

```csharp
// Create immutable enrollment record
var enrollment = new EnrollmentRecord("STU-001", "CS-401", DateTime.UtcNow);

// 'with' expression creates a new record (original unchanged)
var corrected = enrollment with { CourseCode = "CS-402" };

// Value-based equality (compares contents, not references)
var duplicate = new EnrollmentRecord("STU-001", "CS-401", enrollment.EnrolledAt);
Console.WriteLine(enrollment == duplicate);  // True
```

**Key Takeaways**:
- Records are immutable by default
- `with` expression creates modified copies
- Value-based equality compares actual data
- Perfect for historical facts that shouldn't change

### Exercise 3 Part 2: Validated Properties

**Problem**: Courses were created with invalid data (negative capacity, empty titles), causing runtime errors.

**Solution**: Property validation using C# 14 `field` keyword

```csharp
public class Course
{
    public required string Title
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Title cannot be empty.");
    }

    public int Capacity
    {
        get;
        set => field = value > 0
            ? value
            : throw new ArgumentOutOfRangeException("Capacity must be positive.");
    }
}
```

**Key Takeaways**:
- `field` keyword generates backing field automatically
- Validation happens at property level (fail fast)
- Prevents invalid states from ever existing
- Clear error messages for debugging

### Exercise 3 Part 3: Student Model

**Problem**: Student data needs comprehensive validation (age range, GPA limits, required name).

**Solution**: Complete entity with multiple validated properties

```csharp
public class Student
{
    public required string Id { get; init; }  // Immutable after creation
    
    public required string Name
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Name required.");
    }

    public int Age
    {
        get;
        set => field = value is >= 16 and <= 100
            ? value
            : throw new ArgumentOutOfRangeException("Age must be 16-100.");
    }

    public decimal GPA
    {
        get;
        set => field = value is >= 0.0m and <= 4.0m
            ? value
            : throw new ArgumentOutOfRangeException("GPA must be 0.0-4.0.");
    }
}
```

**Key Takeaways**:
- `required` keyword ensures properties are initialized
- `init` accessor allows setting only during construction
- Pattern matching (`is >= 16 and <= 100`) for range validation
- `decimal` for precise GPA calculations

### Exercise 3B: Polymorphic Grade Processing

**Problem**: Different assessment types (quizzes, lab assignments, peer reviews) need different grade calculations. Legacy system used type checking and casting.

**Solution**: Interface-based polymorphism

```csharp
public interface IGradable
{
    string Title { get; }
    decimal CalculateGrade();
}

public class Quiz : IGradable
{
    public decimal CalculateGrade() => 
        (decimal)CorrectAnswers / TotalQuestions * 100m;
}

public class LabAssignment : IGradable
{
    public decimal CalculateGrade() => 
        (FunctionalityScore * 0.7m) + (CodeQualityScore * 0.3m);
}

// Process any gradable assessment polymorphically
void PrintGradeReport(IEnumerable<IGradable> assessments)
{
    foreach (var item in assessments)
        Console.WriteLine($"{item.Title}: {item.CalculateGrade():F2}%");
}
```

**Key Takeaways**:
- Program against interfaces, not concrete types
- Each implementation provides its own calculation logic
- Easy to add new assessment types without changing existing code
- Demonstrates Open/Closed Principle (open for extension, closed for modification)

## Session 2: Query and Classification

Session 2 builds on the data models from Session 1 to add business logic, defensive programming, and data analytics capabilities.

### Exercise 4: Guard Clauses for Defensive Programming

**Problem**: The legacy enrollment system had deeply nested if statements (Pyramid of Doom) that were hard to read and maintain. Error handling was inconsistent.

**Solution**: Guard clauses with fail-fast pattern

```csharp
public EnrollmentRecord ProcessRegistration(Student? student, Course? course)
{
    // Guard #1: Check for null student
    if (student is null)
        throw new ArgumentNullException(nameof(student));

    // Guard #2: Check for null course
    if (course is null)
        throw new ArgumentNullException(nameof(course));

    // Guard #3: Check course capacity
    if (course.Capacity <= 0)
        throw new InvalidOperationException($"Invalid capacity: {course.Capacity}");

    // Guard #4: Check if course is full
    if (course.EnrolledCount >= course.Capacity)
        throw new InvalidOperationException($"Course {course.Code} is full");

    // Happy path: all validations passed
    return new EnrollmentRecord(student.Id, course.Code, DateTime.UtcNow);
}
```

**Key Takeaways**:
- Guard clauses check preconditions at the top of the method
- Fail fast: exit immediately when validation fails
- Keeps the happy path flat and readable (no deep nesting)
- Clear, specific exception messages for debugging
- ArgumentNullException for null parameters (programmer error)
- InvalidOperationException for business rule violations (runtime condition)

### Exercise 4 Part 2: Pattern Matching for Classification

**Problem**: Classifying students by academic standing required verbose if-else chains.

**Solution**: Switch expressions with pattern matching

```csharp
string academicStanding = student.GPA switch
{
    >= 3.5m => "Honors",           // Dean's list
    >= 2.5m => "Good Standing",    // Satisfactory
    >= 2.0m => "Probation",        // At-risk
    _ => "Academic Warning"        // Failing
};
```

**Key Takeaways**:
- Switch expressions are concise and readable
- Pattern matching with relational operators (>=, <, etc.)
- Underscore (_) is the discard pattern (matches everything else)
- Compiler ensures all cases are covered (exhaustive matching)
- Much cleaner than if-else chains for classification logic

### Exercise 5: LINQ Analytics Dashboard

**Problem**: The legacy system required manual loops and temporary variables to analyze student data. Code was verbose and error-prone.

**Solution**: Language Integrated Query (LINQ) for declarative data analysis

```csharp
// Query 1: Find honors students (GPA >= 3.5), sorted by GPA
var leaderboard = students
    .Where(s => s.GPA >= 3.5m)              // Filter
    .OrderByDescending(s => s.GPA)          // Sort
    .Select(s => s.Name)                    // Project
    .ToList();                              // Execute

// Query 2: Calculate class average GPA
decimal averageGpa = students.Average(s => s.GPA);

// Query 3: Group students by academic standing
var standingGroups = students
    .GroupBy(s => s.GPA switch
    {
        >= 3.5m => "Honors",
        >= 2.5m => "Good Standing",
        >= 2.0m => "Probation",
        _ => "Academic Warning"
    });
```

**Key Takeaways**:
- LINQ provides SQL-like querying directly in C#
- Declarative style: describe what you want, not how to get it
- Method chaining for readable query pipelines
- Common operations: Where (filter), OrderBy (sort), Select (project), GroupBy (group)
- Aggregations: Average, Sum, Count, Min, Max
- Deferred execution: query doesn't run until you enumerate results (ToList, foreach, etc.)

### Exercise 5 Part 2: Collection Expressions

**Problem**: Combining arrays required verbose syntax with Array.Copy or LINQ Concat.

**Solution**: C# 12 collection expressions with spread operator

```csharp
string[] backendCourses = ["C#", "ASP.NET Core"];
string[] frontendCourses = ["TypeScript", "Angular"];

// Spread operator (..) combines arrays
string[] allCourses = [..backendCourses, ..frontendCourses, "Capstone"];
```

**Key Takeaways**:
- Collection expressions provide concise array initialization
- Spread operator (..) expands collections inline
- Works with arrays, lists, and other collection types
- More readable than Array.Copy or LINQ Concat
- Part of C# 12's push for more expressive syntax

## Technical Architecture

### Domain Models (`Models.cs`)

**Immutable Records**:
- `EnrollmentRecord`: Historical enrollment facts

**Mutable Entities**:
- `Course`: Courses with validated capacity and title
- `Student`: Students with validated age, GPA, and name

**Interface Contracts**:
- `IGradable`: Contract for gradable assessments

**Assessment Implementations**:
- `Quiz`: Simple percentage calculation
- `LabAssignment`: Weighted score (70% functionality, 30% code quality)

### Business Logic (`EnrollmentService.cs`)

**ProcessRegistration Method**:
- Guard clauses for defensive programming
- Pattern matching for academic standing classification
- Returns immutable EnrollmentRecord on success

### Application Entry Point (`Program.cs`)

Demonstrates all Session 1 and Session 2 exercises with comprehensive examples and explanations.

## Key Concepts Demonstrated

### 1. Null Safety
- Nullable reference types (`string?`)
- Null-conditional operator (`?.`)
- Null-coalescing operators (`??`, `??=`)

### 2. Type Safety
- `decimal` for financial precision
- `double` floating-point limitations
- Exact vs approximate arithmetic

### 3. Immutability
- Records for unchangeable facts
- `with` expressions for modifications
- Value-based equality

### 4. Validation
- Property-level validation with `field` keyword
- Fail-fast error handling
- Clear exception messages

### 5. Polymorphism
- Interface-based design
- Multiple implementations of same contract
- Flexible, extensible architecture

### 6. Defensive Programming
- Guard clauses for precondition validation
- Fail-fast error handling
- Clear exception types and messages

### 7. Pattern Matching
- Switch expressions for classification
- Relational patterns (>=, <, etc.)
- Exhaustive matching with compiler verification

### 8. LINQ Queries
- Declarative data analysis
- Method chaining for readable pipelines
- Filtering, sorting, grouping, and aggregation
- Deferred execution model

## Running the Project

```bash
# Build the project
dotnet build

# Run the application
dotnet run
```

## Expected Output

```
=== Training Management System (TMS) - Session 1: Data Model Foundation ===

--- Exercise 1: Null Safety Patterns ---
Region (conditional): (null)
Region (coalesced): Unassigned
Region (assigned): Addis Ababa

--- Exercise 2: Financial Precision ---
Legacy system (double - WRONG for money):
Total allocated (double): 199999000
Floating-point precision issue: 0.1 + 0.2 = 0.30000000000000004
Should be 0.3, but double shows: 0.30000000000000004

Fixed system (decimal - CORRECT for money):
Total allocated (decimal): 199999000.00
Exact precision: 199999000.00

--- Exercise 3: Data Integrity with Records ---
Original enrollment: EnrollmentRecord { StudentId = STU-001, CourseCode = CS-401, EnrolledAt = ... }
Corrected enrollment: EnrollmentRecord { StudentId = STU-001, CourseCode = CS-402, EnrolledAt = ... }
Original unchanged: EnrollmentRecord { StudentId = STU-001, CourseCode = CS-401, EnrolledAt = ... }
Records are equal: True

--- Exercise 3 Part 2: Property Validation ---
Course: Advanced C# (Capacity: 30)
Validation caught invalid capacity: System constraint: Capacity must be greater than zero.
Validation caught invalid title: Title cannot be empty or whitespace.

--- Exercise 3 Part 3: Student Model ---
Student: Abeba, GPA: 3.8

--- Exercise 3B: Polymorphic Grade Processing ---

--- Grade Report ---
C# Basics: 90.00%
Registration API: 88.50%


=== SESSION 2: Query and Classification ===

--- Exercise 4: Enrollment Service with Guard Clauses ---
Abeba is in Honors.
✓ Enrolled: S1 in CS-401
✓ Guard caught null student: student
✓ Business rule enforced: Course CS-402 is full. Enrolled: 1, Capacity: 1.

--- Exercise 5: Analytics Dashboard with LINQ ---
Found 3 Honors Students:
  - Sara
  - Abeba
  - Yonas

Class Average GPA: 2.93

--- Academic Standing Report ---

Honors (3):
  Abeba - GPA: 3.8
  Sara - GPA: 3.9
  Yonas - GPA: 3.5

Good Standing (2):
  Dawit - GPA: 3.1
  Tesfaye - GPA: 2.9

Probation (2):
  Kidane - GPA: 2.4
  Frehiwot - GPA: 2.0

Academic Warning (1):
  Meron - GPA: 1.8

--- Collection Expressions ---
Full curriculum: C#, ASP.NET Core, TypeScript, Angular, Capstone

=== Sessions 1 and 2 exercises completed successfully! ===
```

## Main Takeaways

### Session 1
1. **Null Safety**: Use nullable reference types and null-coalescing operators to prevent crashes
2. **Financial Precision**: Always use `decimal` for money calculations, never `double`
3. **Immutability**: Use records for facts that shouldn't change after creation
4. **Validation**: Validate data at the property level to prevent invalid states
5. **Polymorphism**: Program against interfaces for flexible, extensible code

### Session 2
6. **Guard Clauses**: Check preconditions at the top of methods and fail fast
7. **Pattern Matching**: Use switch expressions for clean classification logic
8. **LINQ**: Leverage declarative queries for data analysis instead of manual loops
9. **Collection Expressions**: Use modern syntax for combining and initializing collections

## Technology Stack

- **.NET 10**: Latest .NET runtime
- **C# 14**: Modern language features (records, field keyword, pattern matching)
- **C# 12**: Collection expressions with spread operator
- **Nullable Reference Types**: Compile-time null safety
- **Record Types**: Immutable data structures
- **Interface-Based Design**: Polymorphic behavior
- **LINQ**: Language Integrated Query for data analysis

## Project Structure

```
TmsCore/
├── Program.cs              # Sessions 1 & 2 demonstrations
├── Models.cs               # Domain models and interfaces
├── EnrollmentService.cs    # Business logic with guard clauses
├── TmsCore.csproj          # Project configuration
└── README.md               # This file
```

## Next Steps

Session 3 will add:
- Async/await for parallel data loading
- Custom exceptions for domain-specific errors
- Integration reporting with performance metrics

## License

Educational project for C# 14 Essentials Module 1 Lab Sessions 1 and 2.
