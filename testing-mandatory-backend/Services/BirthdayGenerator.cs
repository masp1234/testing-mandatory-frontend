﻿using testing_mandatory_backend.Services;

public class BirthdayGenerator: IBirthdayGenerator
{
    public DateTime GenerateRandomBirthday() {
        DateTime today = DateTime.Today;
        DateTime earliestDate = today.AddYears(-100);
        DateTime latestDate = today.AddYears(-18);

        Random random = new Random();
        int range = (latestDate - earliestDate).Days;
        DateTime randomDate = earliestDate.AddDays(random.Next(range));

        return randomDate;
    }

}
