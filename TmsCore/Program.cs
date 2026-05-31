/// <summary>
/// Training Management System (TMS) - Sessions 1 & 2 Implementation
/// 
/// This program demonstrates exercises from Module 1 Lab Sessions 1 and 2:
/// 
/// SESSION 1: Data Model Foundation
/// - Exercise 1: Null safety patterns with nullable reference types
/// - Exercise 2: Financial precision with decimal vs double
/// - Exercise 3: Data integrity with immutable records and validated properties
/// - Exercise 3B: Polymorphic grade processing with interfaces
/// 
/// SESSION 2: Query and Classification
/// - Exercise 4: Guard clauses for defensive programming
/// - Exercise 5: LINQ analytics dashboard with filtering, sorting, and grouping
/// </summary>

Console.WriteLine("=== Training Management System (TMS) - Session 1: Data Model Foundation ===\n");

// ============================================================================
// EXERCISE 1: NULL SAFETY PATTERNS
// ============================================================================
// Demonstrates C# nullable reference types and null-coalescing operators
// These patterns prevent NullReferenceException - one of the most common bugs

Console.WriteLine("--- Exercise 1: Null Safety Patterns ---");

// Scenario: A student's region might not be specified during registration
string? region = null;  // The ? indicates this variable can hold null

// Pattern 1: Null-conditional operator (?.)
// Safely calls ToUpper() only if region is not null
// If region is null, the entire expression evaluates to null (no exception thrown)
string? upperRegion = region?.ToUpper();
Console.WriteLine($"Region (conditional): {upperRegion ?? "(null)"}");

// Pattern 2: Null-coalescing operator (??)
// Provides a default value when the left side is null
// If region is null, use "Unassigned" instead
string displayRegion = region ?? "Unassigned";
Console.WriteLine($"Region (coalesced): {displayRegion}");

// Pattern 3: Null-coalescing assignment operator (??=)
// Assigns a value only if the variable is currently null
// If region is null, assign "Addis Ababa"; otherwise keep existing value
region ??= "Addis Ababa";
Console.WriteLine($"Region (assigned): {region}");

// ============================================================================
// EXERCISE 2: FINANCIAL PRECISION
// ============================================================================
// Demonstrates why decimal is required for financial calculations
// double has floating-point precision errors that corrupt money calculations

Console.WriteLine("\n--- Exercise 2: Financial Precision ---");

// THE PROBLEM: Using double for money (WRONG!)
Console.WriteLine("Legacy system (double - WRONG for money):");
double legacyGrantPerStudent = 1999.99;
double legacyTotalAllocation = legacyGrantPerStudent * 100_000;
Console.WriteLine($"Total allocated (double): {legacyTotalAllocation}");

// Demonstrate the famous floating-point precision problem
double testValue = 0.1 + 0.2;
Console.WriteLine($"Floating-point precision issue: 0.1 + 0.2 = {testValue}");
Console.WriteLine($"Should be 0.3, but double shows: {testValue:R}");

// THE SOLUTION: Using decimal for money (CORRECT!)
Console.WriteLine("\nFixed system (decimal - CORRECT for money):");
decimal grantPerStudent = 1999.99m;  // The 'm' suffix indicates decimal literal
decimal totalAllocation = grantPerStudent * 100_000m;
Console.WriteLine($"Total allocated (decimal): {totalAllocation}");
Console.WriteLine($"Exact precision: {totalAllocation:F2}");

// KEY TAKEAWAY: Always use decimal for money, percentages, and financial calculations
// Use double only for scientific calculations where small precision errors are acceptable

// ============================================================================
// EXERCISE 3: DATA INTEGRITY WITH IMMUTABLE RECORDS
// ============================================================================
// Demonstrates C# records for immutable data that cannot be accidentally modified
// Records provide value-based equality and built-in immutability

Console.WriteLine("\n--- Exercise 3: Data Integrity with Records ---");

// Create an enrollment record - immutable by design
var enrollment = new EnrollmentRecord("STU-001", "CS-401", DateTime.UtcNow);
Console.WriteLine($"Original enrollment: {enrollment}");

// The 'with' expression creates a new record with modified properties
// The original record remains unchanged (immutability)
var correctedEnrollment = enrollment with { CourseCode = "CS-402" };
Console.WriteLine($"Corrected enrollment: {correctedEnrollment}");
Console.WriteLine($"Original unchanged: {enrollment}");

// Records use value-based equality (compare contents, not references)
var duplicate = new EnrollmentRecord("STU-001", "CS-401", enrollment.EnrolledAt);
Console.WriteLine($"Records are equal: {enrollment == duplicate}");

// ============================================================================
// EXERCISE 3 PART 2: VALIDATED PROPERTIES
// ============================================================================
// Demonstrates property validation using C# 14 'field' keyword
// Ensures data integrity by preventing invalid states

Console.WriteLine("\n--- Exercise 3 Part 2: Property Validation ---");

// Create a course with valid data
var course = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 };
Console.WriteLine($"Course: {course.Title} (Capacity: {course.Capacity})");

// Attempt to set invalid capacity - validation will throw exception
try 
{ 
    course.Capacity = -5;  // Negative capacity makes no sense
}
catch (ArgumentOutOfRangeException ex)
{ 
    Console.WriteLine($"Validation caught invalid capacity: {ex.Message}"); 
}

// Attempt to set empty title - validation will throw exception
try 
{ 
    course.Title = "";  // Empty title is not allowed
}
catch (ArgumentException ex)
{ 
    Console.WriteLine($"Validation caught invalid title: {ex.Message}"); 
}

// KEY TAKEAWAY: Validate data at the property level to prevent invalid states
// This is called "defensive programming" - fail fast with clear error messages

// ============================================================================
// EXERCISE 3 PART 3: STUDENT MODEL WITH VALIDATION
// ============================================================================
// Demonstrates a complete entity with multiple validated properties

Console.WriteLine("\n--- Exercise 3 Part 3: Student Model ---");

// Create a student with all required properties
var student = new Student 
{ 
    Id = "S1", 
    Name = "Abeba", 
    Age = 20, 
    GPA = 3.8m  // Using decimal for precise GPA calculation
};
Console.WriteLine($"Student: {student.Name}, GPA: {student.GPA}");

// The Student class validates:
// - Name cannot be empty or whitespace
// - Age must be between 16 and 100
// - GPA must be between 0.0 and 4.0

// ============================================================================
// EXERCISE 3B: POLYMORPHIC INTERFACE CONTRACTS
// ============================================================================
// Demonstrates programming against interfaces for flexible, extensible code
// Different assessment types (Quiz, LabAssignment) implement the same interface

Console.WriteLine("\n--- Exercise 3B: Polymorphic Grade Processing ---");

// Create an array of different assessment types
// Both Quiz and LabAssignment implement IGradable interface
IGradable[] cohortAssessments = 
[
    new Quiz 
    { 
        Title = "C# Basics", 
        CorrectAnswers = 18, 
        TotalQuestions = 20 
    },
    new LabAssignment 
    { 
        Title = "Registration API", 
        FunctionalityScore = 90m, 
        CodeQualityScore = 85m 
    }
];

// Process all assessments polymorphically
// The method doesn't need to know the specific type - it just calls CalculateGrade()
PrintGradeReport(cohortAssessments);

// ============================================================================
// SESSION 2: QUERY AND CLASSIFICATION
// ============================================================================
// Session 2 builds on Session 1's data models to add business logic and analytics

Console.WriteLine("\n\n=== SESSION 2: Query and Classification ===\n");

// ============================================================================
// EXERCISE 4: GUARD CLAUSES FOR DEFENSIVE PROGRAMMING
// ============================================================================
// Demonstrates the "fail fast" pattern using guard clauses
// Instead of nested if statements, check preconditions at the top and exit immediately

Console.WriteLine("--- Exercise 4: Enrollment Service with Guard Clauses ---");

// Create the enrollment service
var service = new EnrollmentService();

// Create valid test data
var validStudent = new Student { Id = "S1", Name = "Abeba", Age = 20, GPA = 3.8m };
var validCourse = new Course { Code = "CS-401", Title = "Advanced C#", Capacity = 30 };

// Successful registration
var result = service.ProcessRegistration(validStudent, validCourse);
Console.WriteLine($"✓ Enrolled: {result.StudentId} in {result.CourseCode}");

// Test guard clause: null student
try 
{ 
    service.ProcessRegistration(null, validCourse); 
}
catch (ArgumentNullException ex)
{ 
    Console.WriteLine($"✓ Guard caught null student: {ex.ParamName}"); 
}

// Test business rule: course at capacity
var fullCourse = new Course { Code = "CS-402", Title = "Full Course", Capacity = 1 };
fullCourse.EnrolledCount = 1;  // Make it full

try 
{ 
    service.ProcessRegistration(validStudent, fullCourse); 
}
catch (InvalidOperationException ex)
{ 
    Console.WriteLine($"✓ Business rule enforced: {ex.Message}"); 
}

// ============================================================================
// EXERCISE 5: LINQ ANALYTICS DASHBOARD
// ============================================================================
// Demonstrates Language Integrated Query (LINQ) for data analysis
// LINQ provides SQL-like querying capabilities directly in C#

Console.WriteLine("\n--- Exercise 5: Analytics Dashboard with LINQ ---");

// Create a collection of students for analysis
List<Student> students = 
[
    new Student { Id = "S1", Name = "Abeba", Age = 22, GPA = 3.8m },
    new Student { Id = "S2", Name = "Kidane", Age = 21, GPA = 2.4m },
    new Student { Id = "S3", Name = "Dawit", Age = 20, GPA = 3.1m },
    new Student { Id = "S4", Name = "Sara", Age = 23, GPA = 3.9m },
    new Student { Id = "S5", Name = "Frehiwot", Age = 19, GPA = 2.0m },
    new Student { Id = "S6", Name = "Yonas", Age = 24, GPA = 3.5m },
    new Student { Id = "S7", Name = "Meron", Age = 22, GPA = 1.8m },
    new Student { Id = "S8", Name = "Tesfaye", Age = 21, GPA = 2.9m }
];

// LINQ Query 1: Find honors students (GPA >= 3.5)
// Demonstrates: Where (filter), OrderByDescending (sort), Select (project), ToList (execute)
var leaderboard = students
    .Where(s => s.GPA >= 3.5m)              // Filter: only high achievers
    .OrderByDescending(s => s.GPA)          // Sort: highest GPA first
    .Select(s => s.Name)                    // Project: extract just the name
    .ToList();                              // Execute: materialize results

Console.WriteLine($"Found {leaderboard.Count} Honors Students:");
foreach (var name in leaderboard)
    Console.WriteLine($"  - {name}");

// LINQ Query 2: Calculate class average GPA
// Demonstrates: Average aggregation function
decimal averageGpa = students.Average(s => s.GPA);
Console.WriteLine($"\nClass Average GPA: {averageGpa:F2}");

// LINQ Query 3: Group students by academic standing
// Demonstrates: GroupBy with switch expression for classification
var standingGroups = students
    .GroupBy(s => s.GPA switch
    {
        >= 3.5m => "Honors",              // Dean's list
        >= 2.5m => "Good Standing",       // Satisfactory progress
        >= 2.0m => "Probation",           // At-risk
        _ => "Academic Warning"           // Failing
    });

Console.WriteLine("\n--- Academic Standing Report ---");
foreach (var group in standingGroups)
{
    Console.WriteLine($"\n{group.Key} ({group.Count()}):");
    foreach (var s in group)
        Console.WriteLine($"  {s.Name} - GPA: {s.GPA}");
}

// ============================================================================
// EXERCISE 5 PART 2: COLLECTION EXPRESSIONS
// ============================================================================
// Demonstrates C# 12 collection expressions with spread operator (..)
// Provides concise syntax for combining collections

Console.WriteLine("\n--- Collection Expressions ---");

string[] backendCourses = ["C#", "ASP.NET Core"];
string[] frontendCourses = ["TypeScript", "Angular"];

// Spread operator (..) combines arrays into a new array
string[] allCourses = [..backendCourses, ..frontendCourses, "Capstone"];
Console.WriteLine($"Full curriculum: {string.Join(", ", allCourses)}");

Console.WriteLine("\n=== Sessions 1 and 2 exercises completed successfully! ===");

// ============================================================================
// HELPER METHODS
// ============================================================================

/// <summary>
/// Polymorphic method that processes any collection of IGradable assessments
/// Demonstrates programming against interfaces instead of concrete types
/// This method works with Quiz, LabAssignment, or any future IGradable type
/// </summary>
/// <param name="assessments">Collection of gradable assessments</param>
void PrintGradeReport(IEnumerable<IGradable> assessments)
{
    Console.WriteLine("\n--- Grade Report ---");
    foreach (var item in assessments)
    {
        // Polymorphism in action: CalculateGrade() behaves differently
        // for Quiz (simple percentage) vs LabAssignment (weighted score)
        Console.WriteLine($"{item.Title}: {item.CalculateGrade():F2}%");
    }
}
