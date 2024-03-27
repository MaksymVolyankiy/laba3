using System;
using System.Text.Json;

public class Time
{
    private int hours;
    private int minutes;
    private int seconds;

    public int Hours
    {
        get { return hours; }
        set { hours = value; }
    }

    public int Minutes
    {
        get { return minutes; }
        set { minutes = value; }
    }

    public int Seconds
    {
        get { return seconds; }
        set { seconds = value; }
    }

    public Time(int hours, int minutes, int seconds)
    {
        this.hours = hours;
        this.minutes = minutes;
        this.seconds = seconds;
    }

    public int DifferenceInSeconds(Time otherTime)
    {
        int totalSecondsThis = this.hours * 3600 + this.minutes * 60 + this.seconds;
        int totalSecondsOther = otherTime.hours * 3600 + otherTime.minutes * 60 + otherTime.seconds;

        return Math.Abs(totalSecondsThis - totalSecondsOther);
    }

    public void AddSeconds(int secondsToAdd)
    {
        int totalSeconds = this.hours * 3600 + this.minutes * 60 + this.seconds;
        totalSeconds += secondsToAdd;

        if (totalSeconds < 0)
            totalSeconds += 24 * 3600;

        this.hours = (totalSeconds / 3600) % 24;
        this.minutes = (totalSeconds / 60) % 60;
        this.seconds = totalSeconds % 60;
    }

    public void SaveToJsonFile(string filePath)
    {
        string json = JsonSerializer.Serialize(this);
        File.WriteAllText(filePath, json);
    }

    public static Time LoadFromJsonFile(string filePath)
    {
        string jsonString = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<Time>(jsonString);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Time currentTime = new Time(11, 48, 33);
        Time otherTime = new Time(10, 15, 20);

        int differenceInSeconds = currentTime.DifferenceInSeconds(otherTime);
        Console.WriteLine("Рiзниця в секундах: " + differenceInSeconds);

        currentTime.AddSeconds(300);
        Console.WriteLine("Час пiсля того як ми додали 5 хвилин: " + currentTime.Hours + ":" + currentTime.Minutes + ":" + currentTime.Seconds);
        currentTime.SaveToJsonFile("jsonf.json");
        Console.WriteLine("Час з Json:" + Time.LoadFromJsonFile("jsonf.json").Hours + ":" + Time.LoadFromJsonFile("jsonf.json").Minutes + ":" + Time.LoadFromJsonFile("jsonf.json").Seconds);
    }
}