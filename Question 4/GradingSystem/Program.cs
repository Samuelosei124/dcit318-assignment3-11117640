using System;
using System.Collections.Generic;
using System.IO;

// Custom exception for invalid score format
public class InvalidScoreFormatException : Exception
{
    public InvalidScoreFormatException(string message) : base(message) { }
}

// Custom exception for missing fields
public class MissingFieldException : Exception
{
    public MissingFieldException(string message) : base(message) { }
}

// Student class definition
public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public int Score { get; set; }

    public Student(int id, string fullName, int score)
    {
        Id = id;
        FullName = fullName;
        Score = score;
    }

    public string GetGrade()
    {
        if (Score >= 80 && Score <= 100) return "A";
        if (Score >= 70 && Score <= 79) return "B";
        if (Score >= 60 && Score <= 69) return "C";
        if (Score >= 50 && Score <= 59) return "D";
        return "F";
    }
}

// Processor class for reading and writing student results
public class StudentResultProcessor
{
    public List<Student> ReadStudentsFromFile(string inputFilePath)
    {
        var students = new List<Student>();
        using (StreamReader reader = new StreamReader(inputFilePath))
        {
            string line;
            int lineNumber = 0;
            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;
                var fields = line.Split(',');
                if (fields.Length != 3)
                {
                    throw new MissingFieldException($"Line {lineNumber}: Expected 3 fields but got {fields.Length}.");
                }

                int id, score;
                string fullName = fields[1].Trim();
                if (!int.TryParse(fields[0].Trim(), out id))
                {
                    throw new InvalidScoreFormatException($"Line {lineNumber}: Invalid student ID format: '{fields[0]}'");
                }
                if (!int.TryParse(fields[2].Trim(), out score))
                {
                    throw new InvalidScoreFormatException($"Line {lineNumber}: Invalid score format: '{fields[2]}'");
                }
                students.Add(new Student(id, fullName, score));
            }
        }
        return students;
    }

    public void WriteReportToFile(List<Student> students, string outputFilePath)
    {
        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            foreach (var student in students)
            {
                writer.WriteLine($"{student.FullName} (ID: {student.Id}): Score = {student.Score}, Grade = {student.GetGrade()}");
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        string inputFilePath = "input.txt"; // Change as needed
        string outputFilePath = "report.txt"; // Change as needed
        var processor = new StudentResultProcessor();
        try
        {
            var students = processor.ReadStudentsFromFile(inputFilePath);
            processor.WriteReportToFile(students, outputFilePath);
            Console.WriteLine($"Report generated successfully to '{outputFilePath}'.");

            // Print report to terminal
            foreach (var student in students)
            {
                Console.WriteLine($"{student.FullName} (ID: {student.Id}): Score = {student.Score}, Grade = {student.GetGrade()}");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: Input file '{inputFilePath}' not found.");
        }
        catch (InvalidScoreFormatException ex)
        {
            Console.WriteLine("Invalid score format: " + ex.Message);
        }
        catch (MissingFieldException ex)
        {
            Console.WriteLine("Missing field: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred: " + ex.Message);
        }
    }
}
