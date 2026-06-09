# Training Management System (TMS)

## Project Structure

```
TMS/
├── TmsCore/          # C# .NET 10 - Backend Core Logic
│   ├── Models.cs
│   ├── EnrollmentService.cs
│   ├── Program.cs
│   └── README.md
│
└── TmsClient/        # TypeScript 
    ├── index.ts
    ├── models/
    └── README.md
```

## TmsCore - C# Backend (Module 1)

**Technology**: C# 14, .NET 10

**Sessions Implemented**:
- **Session 1**: Data Model Foundation (Null safety, decimal precision, immutable records)
- **Session 2**: Query and Classification (Guard clauses, pattern matching, LINQ)
- **Session 3**: Async and Resilience (Async/await, custom exceptions, parallel loading)

**Key Features**:
- Null-safe data models with validated properties
- Financial precision with decimal types
- Immutable enrollment records
- Polymorphic grade calculation
- Guard clauses for defensive programming
- LINQ analytics dashboard
- Async parallel data loading
- Custom domain exceptions

**Running TmsCore**:
```bash
cd TmsCore
dotnet build
dotnet run
```

See [TmsCore/README.md](TmsCore/README.md) for detailed documentation.

## TmsClient - TypeScript  (Module 2)

**Technology**: TypeScript 6.0, Temporal API

**Sessions Implemented**:
- **Session 1**: TypeScript Basics (Type safety, interfaces, type inference)
- **Session 2**: Advanced Types (Discriminated unions, generics, Temporal API)

**Key Features**:
- Discriminated unions for type-safe variants
- Generic API response wrappers
- State machine with exhaustive checking
- Modern date/time handling with Temporal API
- Immutable data structures
- Type-safe grade calculations

**Running TmsClient**:
```bash
cd TmsClient
npm install
npx tsc
node index.js
```

See [TmsClient/README.md](TmsClient/README.md) for detailed documentation.

## Git Branching Strategy

This repository follows **Git Flow** with incremental feature branches:

### Main Branches
- **main**: all code


### Feature Branches (Incremental)

**TmsCore (Module 1)**:
- `feature/m1-session-1-data-model`: Session 1 only
- `feature/m1-session-2-query-classification`: Sessions 1 + 2
- `feature/m1-session-3-async-resilience`: Sessions 1 + 2 + 3

**TmsClient (Module 2)**:
- `feature/m2-session-1-s`: Session 1 only
- `feature/m2-session-2: Sessions 1 + 2

Each branch builds incrementally on the previous one, demonstrating professional feature development workflow.


