# Training Management System (TMS) - Client Library

## Module 2 Lab Session 1: TypeScript Basics

A professional TypeScript implementation demonstrating basic type annotations, interface design, type safety, custom type guards, and defensive JSON parsing.

## Project Overview

This project implements the basic client-side models for a Training Management System using TypeScript. It establishes essential types and runtime validation layers.

## What Problem Does This Solve?

Legacy JavaScript applications suffer from:
1. **Type Confusion**: Runtime errors because properties are accessed on objects that don't match the expected schema.
2. **Missing Properties**: No way to guarantee at runtime or compile-time that an external API response actually has the fields we expect.
3. **No Automatic Documentation**: Hard to know what fields objects have without reading source code or adding dynamic runtime checks.

This implementation demonstrates type annotations, structural typing, custom type guards, and reliable parsing to prevent these bugs completely.

## Exercises Implemented

### Exercise 1: TypeScript Annotations and Interfaces
- Models the `Student` interface with optional `gpa` and modern date/time using the Temporal API (`Temporal.Instant`).
- Models the `Course` interface with optional `startDate` using `Temporal.PlainDate`.
- Implements structural compatibility checks.

### Exercise 2: Type Guard Enforcement
- Implements `isStudent` custom type guard.
- Uses `value is Student` type predicate to narrow types safely at runtime.

### Exercise 3: Robust Student Parsing
- Implements `parseStudent` that takes an `unknown` payload (e.g., from network/JSON) and dynamically checks types before casting.
- Throws meaningful `TypeError` exceptions if the payload is malformed.

## Running the Project

To compile and run:

```bash
cd TmsClient
npm install
npx tsc
node index.js
```

### Expected Output
```
=== Training Management System (TMS) Client - Session 1: TypeScript Basics ===

--- Exercise 1: Creating Student and Course Instances ---
Course Created: Introduction to TypeScript (ID: CRS-101, Capacity: 30)
Student Created: Dawit Bekele (ID: STU-001, GPA: 3.8)

--- Exercise 2: Type Guard Enforcement ---
✓ Object is a valid Student: Abeba Kebede (ID: STU-999)
✗ Object is not a valid Student (Correct behavior)

--- Exercise 3: Robust Student Parsing ---
✓ Parse Successful: Kidane Wolde (ID: STU-002)
  Enrollment Date: 2026-05-31T11:56:04.123456789Z
✓ Catch block executed correctly (Expected parse error): Expected name to be a string, received undefined

=== Session 1 TypeScript exercises completed successfully! ===
```
