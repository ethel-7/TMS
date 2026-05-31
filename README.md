# Training Management System (TMS)

A comprehensive training management system demonstrating modern software development practices across multiple technology stacks.

## Project Structure

```
TMS/
├── TmsCore/          # C# .NET 10 - Backend Core Logic
│   ├── Models.cs
│   ├── EnrollmentService.cs
│   ├── Program.cs
│   └── README.md
│
└── TmsClient/        # TypeScript - Frontend Type System
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

## TmsClient - TypeScript Frontend (Module 2)

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
- **main**: Production-ready code
- **develop**: Integration branch for all features

### Feature Branches (Incremental)

**TmsCore (Module 1)**:
- `feature/m1-session-1-data-model`: Session 1 only
- `feature/m1-session-2-query-classification`: Sessions 1 + 2
- `feature/m1-session-3-async-resilience`: Sessions 1 + 2 + 3

**TmsClient (Module 2)**:
- `feature/m2-session-1-typescript-basics`: Session 1 only
- `feature/m2-session-2-unions-generics`: Sessions 1 + 2

Each branch builds incrementally on the previous one, demonstrating professional feature development workflow.

## Technology Stack

### Backend (TmsCore)
- .NET 10
- C# 14
- Nullable Reference Types
- Record Types
- LINQ
- Async/Await

### Frontend (TmsClient)
- TypeScript 6.0
- Temporal API (Polyfill)
- Discriminated Unions
- Generics
- Node.js

## Key Concepts Demonstrated

### Software Engineering Principles
- **Type Safety**: Compile-time error prevention
- **Immutability**: Preventing accidental data modification
- **Defensive Programming**: Guard clauses and validation
- **Separation of Concerns**: Clear domain model separation
- **DRY Principle**: Reusable generic code

### Design Patterns
- **Discriminated Unions**: Type-safe variant types
- **State Machine**: Impossible states made unrepresentable
- **Repository Pattern**: Data access abstraction
- **Strategy Pattern**: Polymorphic grade calculation

### Modern Practices
- **Async/Await**: Non-blocking I/O operations
- **LINQ**: Declarative data queries
- **Pattern Matching**: Clean classification logic
- **Exhaustive Checking**: Compiler-enforced completeness

## Development Workflow

### Creating a New Feature

```bash
# Start from develop
git checkout develop
git pull origin develop

# Create feature branch
git checkout -b feature/my-feature

# Make changes, commit
git add .
git commit -m "feat: description"

# Push to remote
git push -u origin feature/my-feature

# Create pull request on GitHub
```

### Merging Features

```bash
# Update develop
git checkout develop
git pull origin develop

# Merge feature (no fast-forward to preserve history)
git merge --no-ff feature/my-feature

# Push to remote
git push origin develop
```

## Running Tests

### TmsCore
```bash
cd TmsCore
dotnet build
dotnet run
```

### TmsClient
```bash
cd TmsClient
npm install
npm test  # If tests are configured
```

## Main Takeaways

### From TmsCore (C#)
1. Use `decimal` for financial calculations, never `double`
2. Leverage nullable reference types for null safety
3. Immutable records for historical data
4. Guard clauses for clean, readable validation
5. LINQ for declarative data analysis
6. Async/await for scalable I/O operations

### From TmsClient (TypeScript)
1. Discriminated unions make impossible states unrepresentable
2. Generics enable type-safe reusable code
3. Temporal API solves date/time complexity
4. Exhaustive checking ensures all cases are handled
5. Type narrowing provides compile-time safety

## License

Educational project demonstrating modern software development practices.

## Authors

- **Module 1 (TmsCore)**: C# 14 Essentials Lab Sessions 1-3
- **Module 2 (TmsClient)**: TypeScript Essentials Lab Sessions 1-2

## Version History

- **v1.0.0**: Complete implementation of Module 1 (Sessions 1-3) and Module 2 (Sessions 1-2)

## Contributing

This is an educational project. For contribution guidelines, see individual project READMEs.
