import { Temporal } from "@js-temporal/polyfill";
import { Student, isStudent, parseStudent } from "./models/student.model";
import { Course } from "./models/course.model";

console.log("=== Training Management System (TMS) Client - Session 1: TypeScript Basics ===\n");

// ============================================================================
// EXERCISE 1: TYPESCRIPT TYPES AND INTERFACES
// ============================================================================
console.log("--- Exercise 1: Creating Student and Course Instances ---");

const course1: Course = {
    id: "CRS-101",
    title: "Introduction to TypeScript",
    capacity: 30,
    startDate: Temporal.PlainDate.from("2026-09-01"),
};

const student1: Student = {
    id: "STU-001",
    name: "Dawit Bekele",
    enrollmentDate: Temporal.Now.instant(),
    gpa: 3.8,
};

console.log(`Course Created: ${course1.title} (ID: ${course1.id}, Capacity: ${course1.capacity})`);
console.log(`Student Created: ${student1.name} (ID: ${student1.id}, GPA: ${student1.gpa})`);

// ============================================================================
// EXERCISE 2: TYPE GUARD ENFORCEMENT
// ============================================================================
console.log("\n--- Exercise 2: Type Guard Enforcement ---");

const unknownObject: unknown = {
    id: "STU-999",
    name: "Abeba Kebede",
    enrollmentDate: Temporal.Now.instant(),
};

if (isStudent(unknownObject)) {
    console.log(`✓ Object is a valid Student: ${unknownObject.name} (ID: ${unknownObject.id})`);
} else {
    console.log("✗ Object is not a valid Student");
}

const invalidObject: unknown = {
    title: "Not a Student",
    capacity: 10,
};

if (isStudent(invalidObject)) {
    console.log("✓ Object is a valid Student");
} else {
    console.log("✗ Object is not a valid Student (Correct behavior)");
}

// ============================================================================
// EXERCISE 3: ROBUST STUDENT PARSING
// ============================================================================
console.log("\n--- Exercise 3: Robust Student Parsing ---");

const validRawJson = {
    id: "STU-002",
    name: "Kidane Wolde",
};

try {
    const parsedStudent = parseStudent(validRawJson);
    console.log(`✓ Parse Successful: ${parsedStudent.name} (ID: ${parsedStudent.id})`);
    console.log(`  Enrollment Date: ${parsedStudent.enrollmentDate}`);
} catch (error) {
    if (error instanceof Error) {
        console.error(`✗ Parse Failed: ${error.message}`);
    }
}

const invalidRawJson = {
    id: "STU-003",
    // Missing 'name' property
};

try {
    const parsedStudent = parseStudent(invalidRawJson);
    console.log(`✓ Parse Successful: ${parsedStudent.name}`);
} catch (error) {
    if (error instanceof Error) {
        console.log(`✓ Catch block executed correctly (Expected parse error): ${error.message}`);
    }
}

console.log("\n=== Session 1 TypeScript exercises completed successfully! ===");
