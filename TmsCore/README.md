# Training Management System (TMS) - Core Logic Engine

## Sessions 1 & 2: Data Model Foundation and Query Classification

A professional C# 14 (.NET 10) implementation demonstrating modern features for building robust data models, defensive programming with guard clauses, and advanced data analytics with LINQ.

## Project Overview

This project implements the core domain models, business logic, and analytics for the Training Management System.

- **Session 1** establishes the foundation: Null safety, financial precision, immutable records, validated properties, and grade interface contract.
- **Session 2** builds on this: Adding defensive programming (guard clauses), academic standing classification using C# pattern matching (switch expressions), collection expressions with spread operator, and comprehensive LINQ querying.

## Key Exercises Implemented (Sessions 1 & 2)

### Session 1 Features
- **Exercise 1: Null Safety Patterns** — Safe navigation (`?.`), coalescing (`??`), and assignment (`??=`).
- **Exercise 2: Financial Precision** — Base-10 arithmetic using `decimal` for exact financial calculations.
- **Exercise 3: Immutable Records** — Creates `EnrollmentRecord` using C# records for value-based equality and immutability.
- **Exercise 3 Part 2 & 3: Validated Entity Models** — Concise validation in `Course` and `Student` using C# 14 `field` keyword.
- **Exercise 3B: Polymorphic Interface Contracts** — `IGradable` interface contract with polymorphic quiz and lab grading.

### Session 2 Features
- **Exercise 4: Guard Clauses for Defensive Programming** — Uses `EnrollmentService` to validate student registration preconditions (null checks, course capacity constraints) using the "fail-fast" paradigm.
- **Exercise 5: LINQ Analytics Dashboard** — Leverages Language Integrated Query to build a statistics dashboard:
  - Leaders Board (Filtering honors students, sorting, projecting).
  - Class Average calculation.
  - Academic standing classification grouping using a switch expression.
- **Exercise 5 Part 2: Collection Expressions** — Demonstrates C# 12 collection expressions and the spread operator (`..`) to cleanly combine arrays.

## Build and Run Instructions

To compile and run the Sessions 1 & 2 implementation:

```bash
cd TmsCore
dotnet build
dotnet run
```

### Expected Output
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
Original enrollment: EnrollmentRecord { StudentId = STU-001, CourseCode = CS-401, EnrolledAt = 5/31/2026 11:39:51 AM }
Corrected enrollment: EnrollmentRecord { StudentId = STU-001, CourseCode = CS-402, EnrolledAt = 5/31/2026 11:39:51 AM }
Original unchanged: EnrollmentRecord { StudentId = STU-001, CourseCode = CS-401, EnrolledAt = 5/31/2026 11:39:51 AM }
Records are equal: True

--- Exercise 3 Part 2: Property Validation ---
Course: Advanced C# (Capacity: 30)
Validation caught invalid capacity: System constraint: Capacity must be greater than zero. (Parameter 'value')
Validation caught invalid title: Title cannot be empty or whitespace. (Parameter 'value')

--- Exercise 3 Part 3: Student Model ---
Student: Abeba, GPA: 3.8

--- Exercise 3B: Polymorphic Grade Processing ---

--- Grade Report ---
C# Basics: 90.00%
Registration API: 88.50%


=== SESSION 2: Query and Classification ===

--- Exercise 4: Enrollment Service with Guard Clauses ---
✓ Enrolled: S1 in CS-401
✓ Guard caught null student: student
✓ Business rule enforced: Course CS-402 is full. Capacity: 1, Enrolled: 1

--- Exercise 5: Analytics Dashboard with LINQ ---
Found 3 Honors Students:
  - Sara
  - Abeba
  - Yonas

Class Average GPA: 2.86

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
