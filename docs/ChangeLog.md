## [0.1.0] - 2026-08-16
### Added
- Create the base project's skeleton and Multi-layer.
- Setting the database.
- Creating the [[Architecture_Decisions]] file.

---

## [0.2.0] - 2026-08-25
### Added
- Creating Generic Repository Class in the `Infrastructure` to handle the traditional CRUD operations for all Entities.
- Creating Unit Of Work class in the `Infrastructure` to divide perform the process and saving it in the Db.
- Creating the main Entities for the System like :
    - `Teacher`,`Student`,`TeacherQualification`,`Group`,`Subject`,`Grade`,`GroupSchedule`,`StudentEnrollment`.

---

## [0.3.0] - 2026-08-30
### Added
- Create the services of `Teacher` and `Student`.
- Create the Controller of `Teacher` and `Student`.
- Test The Route of `Teacher` and `Student` with its Endpoints.

---

## [0.4.0] - 2026-09-27
### Added
- Create the `Subject` Service .
- Create the `Subject` Controller.
- Test the Route of `api/subject` with its Endpoints (CRUD).

---
## [0.4.1] - 2026-09-28
### Added
- Add extra Check within the `Subjet` :
    - To ignore the same subject's name to register again in the system.

---
## [0.4.2] - 2026-09-28
### Added 
- Handle`InvalidOperationException` exception within the `GlobalExceptionHandling`.

---
## [0.4.3] - 2026-09-28
### Added
- Replace the `GradeName` string by `GradeLevel` enum to maintains the grade's consistency.

---
## [0.4.4] - 2026-09-29
### Added
- Add `CleanString` method to insure each `Name` property within the `DTO` weather create or update will save in a standard format.
    - to avoid the format errors.
    - save the property in a standard format.

---
## [0.4.5] - 2026-09-29
### Added
- Add `GetDuplicateCheckExpression` for create and update logic to receive the check logic from the Caller service
    - Separate the check logic within the services.
    - `BaseService` will perform the logic regardless the logic itself.

---
