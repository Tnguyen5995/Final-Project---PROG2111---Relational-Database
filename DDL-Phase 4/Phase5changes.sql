-- =========================================
-- PHASE 4 - DDL SCRIPT
-- Programmers: Tuan Thanh Nguyen, George Shapka
-- First Version: 2025-12-02
-- =========================================

-- Create the database 
CREATE DATABASE IF NOT EXISTS CourseRegProDB;
USE CourseRegProDB;

-- Drop existing tables
SET FOREIGN_KEY_CHECKS = 0;-- makes it ignore foreign keys

DROP TABLE IF EXISTS CourseEnrollment;
DROP TABLE IF EXISTS InstructorAssignment;
DROP TABLE IF EXISTS ProgramCourse;
DROP TABLE IF EXISTS CourseOffering;
DROP TABLE IF EXISTS Course;
DROP TABLE IF EXISTS Student;
DROP TABLE IF EXISTS Instructor;
DROP TABLE IF EXISTS Program;

SET FOREIGN_KEY_CHECKS = 1;-- make it recognize foreign keys again

-- =========================================
-- Create tables 
-- =========================================

------------------------------------------------------------
-- Program table
------------------------------------------------------------
CREATE TABLE Program
(
    programID        INT          NOT NULL,
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
    studentID        INT          NOT NULL,
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
    courseID          INT          NOT NULL,
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
    offeringID    INT          NOT NULL,
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
    instructorID   INT          NOT NULL,
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

---------------------------------------------
-- pre-load data
----------------------------------------------
INSERT INTO Program (programID, programName, credentialType, durationInTerms, isAvailable)
VALUES
(1, 'Computer Programming', 'Diploma', 4, TRUE),
(2, 'Business Administration', 'Diploma', 4, TRUE),
(3, 'Software Engineering', 'Degree', 8, TRUE);

INSERT INTO Student (studentID, programID, firstName, lastName, emailAddress, dateOfBirth, dateEnrolled)
VALUES
(1001, 1, 'Alice', 'Nguyen', 'alice.nguyen@example.com', '2003-04-12', '2023-09-01'),
(1002, 1, 'Brandon', 'Lee', 'brandon.lee@example.com', '2002-11-08', '2023-09-01'),
(1003, 2, 'Carla', 'Santos', 'carla.santos@example.com', '2001-06-22', '2022-09-01'),
(1004, 3, 'David', 'Miller', 'david.miller@example.com', '2000-02-15', '2024-09-01');

INSERT INTO Course (courseID, courseTitle, courseDescription, creditHours)
VALUES
(101, 'Intro to Programming', 'Basic programming concepts using Python.', 3),
(102, 'Database Systems', 'Relational databases and SQL.', 4),
(103, 'Business Communications', 'Professional communication skills.', 3),
(104, 'Algorithms & Data Structures', 'Core algorithmic principles.', 4),
(105, 'Financial Accounting', 'Fundamentals of accounting.', 3);

INSERT INTO CourseOffering
(offeringID, courseID, termStart, termEnd, academicYear, maxCapacity, selectionCode, roomLocation, deliveryMode, scheduleInfo)
VALUES
(5001, 101, '2025-01-10', '2025-04-20', '2024-2025', 30, 'A01', 'Room B210', 'OnCampus', 'Mon 9-11 AM'),
(5002, 102, '2025-01-10', '2025-04-20', '2024-2025', 25, 'A01', 'Room C115', 'OnCampus', 'Wed 1-4 PM'),
(5003, 103, '2025-01-10', '2025-04-20', '2024-2025', 40, 'B01', NULL, 'Online', 'Tue 6-8 PM'),
(5004, 104, '2025-01-10', '2025-04-20', '2024-2025', 20, 'A01', 'Room D330', 'OnCampus', 'Fri 10-12 AM'),
(5005, 105, '2025-01-10', '2025-04-20', '2024-2025', 35, 'A01', 'Room B120', 'Hybrid', 'Thu 2-4 PM');

INSERT INTO ProgramCourse (programID, courseID, isRequired)
VALUES
-- Computer Programming (Diploma)
(1, 101, TRUE),
(1, 102, TRUE),
(1, 104, TRUE),

-- Business Administration
(2, 103, TRUE),
(2, 105, TRUE),

-- Software Engineering (Degree)
(3, 101, TRUE),
(3, 102, TRUE),
(3, 104, TRUE);

INSERT INTO Instructor (instructorID, firstName, lastName, emailAddress, hireDate, officeLocation)
VALUES
(9001, 'Karen', 'Smith', 'karen.smith@college.edu', '2018-05-10', 'Office A110'),
(9002, 'Leon', 'Brown', 'leon.brown@college.edu', '2020-01-15', 'Office B210'),
(9003, 'Sofia', 'Martinez', 'sofia.martinez@college.edu', '2016-09-01', 'Office C330');

INSERT INTO InstructorAssignment (instructorID, offeringID)
VALUES
(9001, 5001),
(9001, 5002),
(9002, 5003),
(9003, 5004),
(9002, 5005);

INSERT INTO CourseEnrollment (studentID, offeringID, enrollmentStatus, finalGrade)
VALUES
(1001, 5001, 'Enrolled', NULL),
(1001, 5002, 'Enrolled', NULL),
(1002, 5001, 'Completed', 88.50),
(1003, 5003, 'Enrolled', NULL),
(1004, 5004, 'Enrolled', NULL),
(1004, 5002, 'Dropped', NULL);

