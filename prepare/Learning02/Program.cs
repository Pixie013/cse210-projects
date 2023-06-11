using System;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job();
        job1._jobTitle = "Operations Manager";
        job1._company = "Avatar Health";
        job1._startYear = 2019;
        job1._endYear = 2021;

        Job job2 = new Job();
        job2._jobTitle = "General Manager";
        job2._company = "Packratb cc";
        job2._startYear = 2022;
        job2._endYear = 2023;

        Resume myResume = new Resume();
        myResume._name = "Carol Maxwell";

        myResume._jobs.Add(job1);
        myResume._jobs.Add(job2);

        myResume.Display();
    }
}