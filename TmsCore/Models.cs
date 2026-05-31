/// <summary>
/// Domain models for the Training Management System (TMS) - Session 1
/// This file contains all the core data structures for Session 1:
/// - Immutable records for enrollment tracking
/// - Validated entity classes for courses and students
/// - Interface contracts for polymorphic grade processing
/// </summary>

// ============================================================================
// IMMUTABLE DATA RECORDS - Used for facts that don't change after creation
// ============================================================================

/// <summary>
/// Represents an enrollment event - immutable by design to prevent data corruption
/// Once a student is enrolled in a course, this record cannot be modified
/// Uses C# record syntax for automatic immutability and value-based equality
/// </summary>
/// <param name="StudentId">Unique identifier for the student</param>
/// <param name="CourseCode">Course code the student is enrolled in</param>
/// <param name="EnrolledAt">Timestamp when enrollment occurred</param>
public record EnrollmentRecord(string StudentId, string CourseCode, DateTime EnrolledAt);

// ============================================================================
// MUTABLE ENTITY CLASSES - Used for objects that change over time
// ============================================================================

/// <summary>
/// Represents a course in the system with mutable properties
/// Courses can change capacity and titles throughout a semester
/// Uses C# 14 'field' keyword for concise property validation
/// </summary>
public class Course
{
    /// <summary>
    /// Course code - set once during creation and cannot be changed
    /// Uses 'init' accessor to allow setting only during object initialization
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// Course title with validation to prevent empty or whitespace-only values
    /// Uses C# 14 'field' keyword to generate backing field automatically
    /// Throws ArgumentException if invalid title is provided
    /// </summary>
    public required string Title
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Title cannot be empty or whitespace.", nameof(value));
    }

    /// <summary>
    /// Maximum number of students that can enroll in this course
    /// Validates that capacity is always positive (greater than 0)
    /// Uses C# 14 'field' keyword for automatic backing field generation
    /// </summary>
    public int Capacity
    {
        get;
        set => field = value > 0
            ? value
            : throw new ArgumentOutOfRangeException(nameof(value), "System constraint: Capacity must be greater than zero.");
    }

    /// <summary>
    /// Current number of enrolled students
    /// Simple auto-property without validation as it's managed by business logic
    /// </summary>
    public int EnrolledCount { get; set; }
}

/// <summary>
/// Represents a student in the system with validated properties
/// Students have lifecycle and state changes throughout their academic journey
/// </summary>
public class Student
{
    /// <summary>
    /// Unique student identifier - set once and cannot be changed
    /// Uses 'init' accessor for immutability after construction
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Student's full name with validation to prevent empty values
    /// Uses C# 14 'field' keyword for concise validation logic
    /// </summary>
    public required string Name
    {
        get;
        set => field = !string.IsNullOrWhiteSpace(value)
            ? value
            : throw new ArgumentException("Name cannot be empty or whitespace.", nameof(value));
    }

    /// <summary>
    /// Student's age with business rule validation (16-100 years)
    /// Uses C# pattern matching with 'is' keyword for range validation
    /// </summary>
    public int Age
    {
        get;
        set => field = value is >= 16 and <= 100
            ? value
            : throw new ArgumentOutOfRangeException(nameof(value), "Age must be between 16 and 100.");
    }

    /// <summary>
    /// Grade Point Average on a 4.0 scale
    /// Uses decimal type for exact financial/academic calculations (no floating-point drift)
    /// Validates GPA is within standard 0.0-4.0 range
    /// </summary>
    public decimal GPA
    {
        get;
        set => field = value is >= 0.0m and <= 4.0m
            ? value
            : throw new ArgumentOutOfRangeException(nameof(value), "GPA must be between 0.0 and 4.0.");
    }
}

// ============================================================================
// INTERFACE CONTRACTS - Define behavior that multiple types can implement
// ============================================================================

/// <summary>
/// Contract for any assessment that can be graded
/// Enables polymorphic processing of different assessment types
/// (quizzes, lab assignments, peer reviews, etc.)
/// </summary>
public interface IGradable
{
    /// <summary>
    /// Display name for the assessment
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Calculate the final grade for this assessment
    /// Returns a percentage (0-100) as a decimal
    /// </summary>
    /// <returns>Grade as a percentage (0-100)</returns>
    decimal CalculateGrade();
}

// ============================================================================
// ASSESSMENT IMPLEMENTATIONS - Different ways to calculate grades
// ============================================================================

/// <summary>
/// Quiz assessment graded by correct answers out of total questions
/// Simple percentage calculation: (correct / total) * 100
/// </summary>
public class Quiz : IGradable
{
    /// <summary>
    /// Quiz title/name
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// Number of questions answered correctly
    /// </summary>
    public required int CorrectAnswers { get; init; }

    /// <summary>
    /// Total number of questions in the quiz
    /// </summary>
    public required int TotalQuestions { get; init; }

    /// <summary>
    /// Calculate quiz grade as percentage of correct answers
    /// Handles edge case of zero questions to prevent division by zero
    /// </summary>
    /// <returns>Percentage grade (0-100)</returns>
    public decimal CalculateGrade()
    {
        // Guard against division by zero
        if (TotalQuestions == 0) return 0m;
        
        // Cast to decimal to ensure exact calculation (no floating-point errors)
        // Multiply by 100 to convert ratio to percentage
        return (decimal)CorrectAnswers / TotalQuestions * 100m;
    }
}

/// <summary>
/// Lab assignment graded by weighted combination of functionality and code quality
/// Uses industry-standard weighting: 70% functionality, 30% code quality
/// </summary>
public class LabAssignment : IGradable
{
    /// <summary>
    /// Lab assignment title/name
    /// </summary>
    public required string Title { get; init; }

    /// <summary>
    /// Score for functional requirements (0-100)
    /// </summary>
    public required decimal FunctionalityScore { get; init; }

    /// <summary>
    /// Score for code quality aspects (0-100)
    /// </summary>
    public required decimal CodeQualityScore { get; init; }

    /// <summary>
    /// Calculate weighted grade: 70% functionality + 30% code quality
    /// This weighting reflects industry priorities where working code
    /// is more important than perfect code style
    /// </summary>
    /// <returns>Weighted percentage grade (0-100)</returns>
    public decimal CalculateGrade()
    {
        // Industry-standard weighting for software development assessments
        // 70% functionality (does it work?) + 30% code quality (is it maintainable?)
        return (FunctionalityScore * 0.7m) + (CodeQualityScore * 0.3m);
    }
}
