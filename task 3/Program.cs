using System;
using System.Collections.Generic;

class Student
{
    public int StudentId;
    public string Name;
    public int Age;
    public List<Course> Courses = new List<Course>();

    public bool Enroll(Course course)
    {
        if (Courses.Contains(course))
            return false;

        Courses.Add(course);
        return true;
    }

    public string PrintDetails()
    {
        return $"ID: {StudentId}, Name: {Name}, Age: {Age}";
    }
}

class Instructor
{
    public int InstructorId;
    public string Name;
    public string Specialization;

    public string PrintDetails()
    {
        return $"ID: {InstructorId}, Name: {Name}, Specialization: {Specialization}";
    }
}

class Course
{
    public int CourseId;
    public string Title;
    public Instructor Instructor;

    public string PrintDetails()
    {
        return $"ID: {CourseId}, Title: {Title}, Instructor: {Instructor.Name}";
    }
}

class StudentManager
{
    public List<Student> Students = new List<Student>();
    public List<Course> Courses = new List<Course>();
    public List<Instructor> Instructors = new List<Instructor>();

    public bool AddStudent(Student student)
    {
        if (FindStudent(student.StudentId) != null)
            return false;

        Students.Add(student);
        return true;
    }

    public bool AddInstructor(Instructor instructor)
    {
        if (FindInstructor(instructor.InstructorId) != null)
            return false;

        Instructors.Add(instructor);
        return true;
    }

    public bool AddCourse(Course course)
    {
        if (FindCourse(course.CourseId) != null)
            return false;

        Courses.Add(course);
        return true;
    }

    public Student FindStudent(int studentId)
    {
        foreach (Student s in Students)
            if (s.StudentId == studentId)
                return s;

        return null;
    }

    public Course FindCourse(int courseId)
    {
        foreach (Course c in Courses)
            if (c.CourseId == courseId)
                return c;

        return null;
    }

    public Instructor FindInstructor(int instructorId)
    {
        foreach (Instructor i in Instructors)
            if (i.InstructorId == instructorId)
                return i;

        return null;
    }

    public bool EnrollStudentInCourse(int studentId, int courseId)
    {
        Student student = FindStudent(studentId);
        Course course = FindCourse(courseId);

        if (student == null || course == null)
            return false;

        return student.Enroll(course);
    }

    public void ShowStudents()
    {
        foreach (Student s in Students)
            Console.WriteLine(s.PrintDetails());
    }

    public void ShowCourses()
    {
        foreach (Course c in Courses)
            Console.WriteLine(c.PrintDetails());
    }

    public void ShowInstructors()
    {
        foreach (Instructor i in Instructors)
            Console.WriteLine(i.PrintDetails());
    }
}

class Program
{
    static void Main()
    {
        StudentManager school = new StudentManager();
        int choice;

        do
        {
            Console.WriteLine("\n--- Student Management System ---");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Add Instructor");
            Console.WriteLine("3. Add Course");
            Console.WriteLine("4. Enroll Student in Course");
            Console.WriteLine("5. Show All Students");
            Console.WriteLine("6. Show All Courses");
            Console.WriteLine("7. Show All Instructors");
            Console.WriteLine("8. Find Student");
            Console.WriteLine("9. Find Course");
            Console.WriteLine("10. Exit");
            Console.WriteLine("11. Check Student Enrollment");
            Console.WriteLine("12. Find Instructor by Course Name");
            Console.Write("Choose: ");

            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Student s = new Student();

                    Console.Write("Student ID: ");
                    s.StudentId = int.Parse(Console.ReadLine());

                    Console.Write("Name: ");
                    s.Name = Console.ReadLine();

                    Console.Write("Age: ");
                    s.Age = int.Parse(Console.ReadLine());

                    Console.WriteLine(school.AddStudent(s)
                        ? "Student added"
                        : "ID already exists");
                    break;

                case 2:
                    Instructor i = new Instructor();

                    Console.Write("Instructor ID: ");
                    i.InstructorId = int.Parse(Console.ReadLine());

                    Console.Write("Name: ");
                    i.Name = Console.ReadLine();

                    Console.Write("Specialization: ");
                    i.Specialization = Console.ReadLine();

                    Console.WriteLine(school.AddInstructor(i)
                        ? "Instructor added"
                        : "ID already exists");
                    break;

                case 3:
                    Course c = new Course();

                    Console.Write("Course ID: ");
                    c.CourseId = int.Parse(Console.ReadLine());

                    Console.Write("Course Title: ");
                    c.Title = Console.ReadLine();

                    Console.Write("Instructor ID: ");
                    int instructorId = int.Parse(Console.ReadLine());

                    c.Instructor = school.FindInstructor(instructorId);

                    if (c.Instructor == null)
                        Console.WriteLine("Instructor not found");
                    else
                        Console.WriteLine(school.AddCourse(c)
                            ? "Course added"
                            : "ID already exists");
                    break;

                case 4:
                    Console.Write("Student ID: ");
                    int sid = int.Parse(Console.ReadLine());

                    Console.Write("Course ID: ");
                    int cid = int.Parse(Console.ReadLine());

                    Console.WriteLine(school.EnrollStudentInCourse(sid, cid)
                        ? "Enrollment successful"
                        : "Student or course not found, or already enrolled");
                    break;

                case 5:
                    school.ShowStudents();
                    break;

                case 6:
                    school.ShowCourses();
                    break;

                case 7:
                    school.ShowInstructors();
                    break;

                case 8:
                    Console.Write("Enter Student ID or Name: ");
                    string studentSearch = Console.ReadLine();

                    foreach (Student st in school.Students)
                    {
                        if (st.StudentId.ToString() == studentSearch ||
                            st.Name.Equals(studentSearch,
                            StringComparison.OrdinalIgnoreCase))
                            Console.WriteLine(st.PrintDetails());
                    }
                    break;

                case 9:
                    Console.Write("Enter Course ID or Name: ");
                    string courseSearch = Console.ReadLine();

                    foreach (Course co in school.Courses)
                    {
                        if (co.CourseId.ToString() == courseSearch ||
                            co.Title.Equals(courseSearch,
                            StringComparison.OrdinalIgnoreCase))
                            Console.WriteLine(co.PrintDetails());
                    }
                    break;

                case 10:
                    Console.WriteLine("Goodbye!");
                    break;

                case 11:
                    Console.Write("Student ID: ");
                    int studentId = int.Parse(Console.ReadLine());

                    Console.Write("Course ID: ");
                    int courseId = int.Parse(Console.ReadLine());

                    Student foundStudent = school.FindStudent(studentId);
                    Course foundCourse = school.FindCourse(courseId);

                    if (foundStudent != null && foundCourse != null &&
                        foundStudent.Courses.Contains(foundCourse))
                        Console.WriteLine("Student is enrolled");
                    else
                        Console.WriteLine("Student is not enrolled");
                    break;

                case 12:
                    Console.Write("Course Name: ");
                    string title = Console.ReadLine();

                    foreach (Course co in school.Courses)
                    {
                        if (co.Title.Equals(title,
                            StringComparison.OrdinalIgnoreCase))
                            Console.WriteLine(co.Instructor.Name);
                    }
                    break;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

        } while (choice != 10);
    }
}