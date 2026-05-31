/// <summary>
/// Service responsible for processing student course registrations
/// Demonstrates guard clauses and pattern matching for clean, readable code
/// </summary>
public class EnrollmentService
{
    /// <summary>
    /// Process a student registration for a course with comprehensive validation
    /// Uses guard clauses to fail fast and keep the happy path readable
    /// Demonstrates C# pattern matching for academic standing classification
    /// </summary>
    /// <param name="student">Student to register (can be null)</param>
    /// <param name="course">Course to register for (can be null)</param>
    /// <returns>EnrollmentRecord representing the successful registration</returns>
    /// <exception cref="ArgumentNullException">Thrown when student or course is null</exception>
    /// <exception cref="InvalidOperationException">Thrown when course is full or has invalid capacity</exception>
    public EnrollmentRecord ProcessRegistration(Student? student, Course? course)
    {
        // ============================================================================
        // GUARD CLAUSES - Fail fast pattern for precondition validation
        // ============================================================================
        // Instead of nested if statements (Pyramid of Doom), we check each condition
        // at the top and exit immediately if it fails. This keeps the happy path flat.
        
        // Guard #1: Ensure student is provided
        // ArgumentNullException is the standard exception for null parameters
        if (student is null)
            throw new ArgumentNullException(nameof(student), "Student cannot be null for registration.");

        // Guard #2: Ensure course is provided  
        if (course is null)
            throw new ArgumentNullException(nameof(course), "Course cannot be null for registration.");

        // Guard #3: Ensure course has valid capacity
        // InvalidOperationException is used for business rule violations (not programmer errors)
        if (course.Capacity <= 0)
            throw new InvalidOperationException($"Course {course.Code} has invalid capacity: {course.Capacity}. Capacity must be greater than zero.");

        // Guard #4: Ensure course is not full
        // This is a runtime business condition, not a programmer error
        // Use custom CapacityReachedException for domain-specific error handling
        if (course.EnrolledCount >= course.Capacity)
            throw new CapacityReachedException(course.Code);

        // ============================================================================
        // ACADEMIC STANDING CLASSIFICATION - Pattern matching with switch expressions
        // ============================================================================
        // C# switch expressions provide clean, readable classification logic
        // Much cleaner than if-else chains for this type of categorization
        
        string academicStanding = student.GPA switch
        {
            >= 3.5m => "Honors",           // Dean's list level performance
            >= 2.5m => "Good Standing",    // Satisfactory academic progress  
            >= 2.0m => "Probation",        // At-risk, needs improvement
            _ => "Academic Warning"        // Covers all remaining cases (< 2.0m)
        };

        // Log the academic standing for tracking purposes
        Console.WriteLine($"{student.Name} is in {academicStanding}.");

        // ============================================================================
        // SUCCESSFUL REGISTRATION - Create immutable enrollment record
        // ============================================================================
        // All validations passed, create the enrollment record
        // Using DateTime.UtcNow ensures consistent timezone handling across deployments
        return new EnrollmentRecord(student.Id, course.Code, DateTime.UtcNow);
    }
}
