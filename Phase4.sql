-- =========================================
-- PHASE 4 - DDL SCRIPT
-- Programmers: Tuan Thanh Nguyen, George Shapka
-- First Version: 2025-12-02
-- =========================================

-- Create the database 
CREATE DATABASE IF NOT EXISTS CourseRegProDB;
USE CourseRegProDB;

-- Drop existing tables
SET FOREIGN_KEY_CHECKS = 0;--makes it ignore foreign keys

DROP TABLE IF EXISTS CourseEnrollment;
DROP TABLE IF EXISTS InstructorAssignment;
DROP TABLE IF EXISTS ProgramCourse;
DROP TABLE IF EXISTS CourseOffering;
DROP TABLE IF EXISTS Course;
DROP TABLE IF EXISTS Student;
DROP TABLE IF EXISTS Instructor;
DROP TABLE IF EXISTS Program;

SET FOREIGN_KEY_CHECKS = 1;--make it recognize foreign keys again

-- =========================================
-- Create tables 
-- =========================================

------------------------------------------------------------
-- Program table
------------------------------------------------------------
CREATE TABLE Program
(
    programID        INT          NOT NULL AUTO_INCREMENT,
    programName      VARCHAR(100) NOT NULL,
    credentialType   VARCHAR(50)  NOT NULL,   -- e.g., Diploma, Degree
    durationInTerms  TINYINT      NOT NULL,   -- number of academic terms
    isAvailable      BOOLEAN      NOT NULL DEFAULT TRUE,
    CONSTRAINT PK_Program
        PRIMARY KEY (programID)
);

------------------------------------------------------------
-- Student table
------------------------------------------------------------
CREATE TABLE Student
(
    studentID        INT          NOT NULL AUTO_INCREMENT,
    programID        INT          NOT NULL,
    firstName        VARCHAR(50)  NOT NULL,
    lastName         VARCHAR(50)  NOT NULL,
    emailAddress     VARCHAR(100) NOT NULL,
    dateOfBirth      DATE         NULL,
    dateEnrolled     DATE         NOT NULL,
    CONSTRAINT PK_Student
        PRIMARY KEY (studentID),
    CONSTRAINT UQ_Student_Email
        UNIQUE (emailAddress),
    CONSTRAINT FK_Student_Program
        FOREIGN KEY (programID)
        REFERENCES Program (programID)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
);

------------------------------------------------------------
-- Course table
------------------------------------------------------------
CREATE TABLE Course
(
    courseID          INT          NOT NULL AUTO_INCREMENT,
    courseTitle       VARCHAR(100) NOT NULL,
    courseDescription TEXT         NULL,
    creditHours       TINYINT      NOT NULL,
    CONSTRAINT PK_Course
        PRIMARY KEY (courseID)
);

------------------------------------------------------------
-- CourseOffering table
------------------------------------------------------------
CREATE TABLE CourseOffering
(
    offeringID    INT          NOT NULL AUTO_INCREMENT,
    courseID      INT          NOT NULL,
    termStart     DATE         NOT NULL,
    termEnd       DATE         NOT NULL,
    academicYear  VARCHAR(9)   NOT NULL,      -- ex. '2025-2026'
    maxCapacity   INT          NOT NULL,
    selectionCode VARCHAR(10)  NOT NULL,      -- section identifier
    roomLocation  VARCHAR(50)  NULL,
    deliveryMode  VARCHAR(20)  NOT NULL,      -- ex. OnCampus / Online / Hybrid
    scheduleInfo  VARCHAR(100) NULL,          -- ex. 'Mon 9-11 AM'
    CONSTRAINT PK_CourseOffering
        PRIMARY KEY (offeringID),
    CONSTRAINT FK_CourseOffering_Course
        FOREIGN KEY (courseID)
        REFERENCES Course (courseID)
        ON UPDATE CASCADE
        ON DELETE RESTRICT,
    -- Ensure we do not accidentally create the same section twice
    CONSTRAINT UQ_Offering_Course_Term_Section
        UNIQUE (courseID, termStart, termEnd, selectionCode)
);

------------------------------------------------------------
-- ProgramCourse table
------------------------------------------------------------
CREATE TABLE ProgramCourse
(
    programID   INT      NOT NULL,
    courseID    INT      NOT NULL,
    isRequired  BOOLEAN  NOT NULL,
    CONSTRAINT PK_ProgramCourse
        PRIMARY KEY (programID, courseID),
    CONSTRAINT FK_ProgramCourse_Program
        FOREIGN KEY (programID)
        REFERENCES Program (programID)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    CONSTRAINT FK_ProgramCourse_Course
        FOREIGN KEY (courseID)
        REFERENCES Course (courseID)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
);

------------------------------------------------------------
-- Instructor table
------------------------------------------------------------
CREATE TABLE Instructor
(
    instructorID   INT          NOT NULL AUTO_INCREMENT,
    firstName      VARCHAR(50)  NOT NULL,
    lastName       VARCHAR(50)  NOT NULL,
    emailAddress   VARCHAR(100) NOT NULL,
    hireDate       DATE         NULL,
    officeLocation VARCHAR(50)  NULL,
    CONSTRAINT PK_Instructor
        PRIMARY KEY (instructorID),
    CONSTRAINT UQ_Instructor_Email
        UNIQUE (emailAddress)
);

------------------------------------------------------------
-- InstructorAssignment table
------------------------------------------------------------
CREATE TABLE InstructorAssignment
(
    instructorID INT NOT NULL,
    offeringID   INT NOT NULL,
    CONSTRAINT PK_InstructorAssignment
        PRIMARY KEY (instructorID, offeringID),
    CONSTRAINT FK_InstructorAssignment_Instructor
        FOREIGN KEY (instructorID)
        REFERENCES Instructor (instructorID)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    CONSTRAINT FK_InstructorAssignment_Offering
        FOREIGN KEY (offeringID)
        REFERENCES CourseOffering (offeringID)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

------------------------------------------------------------
-- CourseEnrollment table
------------------------------------------------------------
CREATE TABLE CourseEnrollment
(
    studentID        INT          NOT NULL,
    offeringID       INT          NOT NULL,
    enrollmentStatus VARCHAR(20)  NOT NULL,   -- Enrolled / Dropped / Completed / Failed
    finalGrade       DECIMAL(5,2) NULL,
    CONSTRAINT PK_CourseEnrollment
        PRIMARY KEY (studentID, offeringID),
    CONSTRAINT FK_CourseEnrollment_Student
        FOREIGN KEY (studentID)
        REFERENCES Student (studentID)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    CONSTRAINT FK_CourseEnrollment_Offering
        FOREIGN KEY (offeringID)
        REFERENCES CourseOffering (offeringID)
        ON UPDATE CASCADE
        ON DELETE RESTRICT
);