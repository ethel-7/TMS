# Training Management System (TMS) - Core Logic Engine

## Session 1: Data Model Foundation

A professional C# 14 (.NET 10) implementation of the Data Model Foundation, demonstrating modern C# features for building robust, type-safe data models.

## Project Overview

This project implements the core domain models and grade processing interface contract for a Training Management System.

Session 1 establishes a solid foundation with:
1. **Null Safety**: Using C# nullable reference types and null-coalescing operators to prevent `NullReferenceException` crashes.
2. **Financial Precision**: Using the `decimal` type for financial allocation and calculations, avoiding floating-point precision drift.
3. **Data Integrity**: Using C# `record` types for immutable enrollment tracking with value-based equality.
4. **Validated Properties**: Implementing property-level validation using the C# 14 `field` keyword.
5. **Polymorphic Grade Contracts**: Defining a clean interface (`IGradable`) implemented by multiple assessment types (`Quiz`, `LabAssignment`) for flexible processing.

## Key Exercises Implemented

### Exercise 1: Null Safety Patterns
- Safe navigation with null-conditional operator (`?.`)
- Default values with null-coalescing operator (`??`)
- In-place assignment with null-coalescing assignment (`??=`)

### Exercise 2: Financial Precision
- Demonstrates floating-point drift with `double`
- Implements base-10 arithmetic using `decimal` for exact financial calculations

### Exercise 3: Immutable Records
- Creates immutable records for event tracking
- Uses `with` expression for non-destructive mutation
- Verifies value-based equality

### Exercise 3 Part 2 & 3: Validated Entity Models
- `Course`: Implements property-level validation for title and capacity using the `field` keyword
- `Student`: Validates student ID, name, age (16-100), and GPA (0.0-4.0)

### Exercise 3B: Polymorphic Interface Contracts
- `IGradable` interface contract
- `Quiz` implementation (simple percentage score)
- `LabAssignment` implementation (weighted score: 70% functionality, 30% code quality)
- `PrintGradeReport` polymorphic engine

## Build and Run Instructions

To compile and run the Session 1 implementation:

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
Original enrollment: EnrollmentRecord { StudentId = STU-001, CourseCode = CS-401, EnrolledAt = 5/31/2026 11:39:27 AM }
Corrected enrollment: EnrollmentRecord { StudentId = STU-001, CourseCode = CS-402, EnrolledAt = 5/31/2026 11:39:27 AM }
Original unchanged: EnrollmentRecord { StudentId = STU-001, CourseCode = CS-401, EnrolledAt = 5/31/2026 11:39:27 AM }
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

=== Session 1 exercises completed successfully! ===
```
