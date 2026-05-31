import { Temporal } from "@js-temporal/polyfill";

/**
 * Represents a Course in the Training Management System
 * Uses modern readonly properties and optional startDate using Temporal API
 */
export interface Course {
    readonly id: string;
    title: string;
    capacity: number;
    startDate?: Temporal.PlainDate;
}
