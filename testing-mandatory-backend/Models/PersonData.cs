﻿namespace testing_mandatory_backend.Models
{
    public class PersonData
    {
        public FakeAddress FakeAddress { get; set; }

        public Person Person { get; set; }

        public DateTime BirthDay { get; set; }

        public string PhoneNumber { get; set; }

        public string CPR { get; set; }

        public PersonData(
            FakeAddress fakeAddress,
            Person person,
            DateTime birthday,
            string phoneNumber,
            string cpr
            )
        {
            FakeAddress = fakeAddress;
            Person = person;
            BirthDay = birthday;
            PhoneNumber = phoneNumber;
            CPR = cpr;

        }
    }
}
