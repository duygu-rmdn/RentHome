using Database___exercise.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database___exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new ExerciseContext();
            context.Database.Migrate();

            context.CityCodes.Add(new CityCode { Number = 112 });
            context.SaveChanges();
        }
    }
}
