/// <summary>
/// Training Management System (TMS) - Session 1: Data Model Foundation
/// 
/// This program demonstrates exercises from Module 1 Lab Session 1:
/// 
/// SESSION 1: Data Model Foundation
/// - Exercise 1: Null safety patterns with nullable reference types
/// - Exercise 2: Financial precision with decimal vs double
/// - Exercise 3: Data integrity with immutable records and validated properties
/// - Exercise 3B: Polymorphic grade processing with interfaces
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

Console.WriteLine("\n=== Session 1 exercises completed successfully! ===");

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
