﻿using LearningManagementSystem.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagementSystem.Domain.Entities.Courses
{
    public class Rating
    {
        public Guid RatingId { get; private set; }
        public User Student { get; private set; }
        public Course Course { get; private set; }
        public decimal Value { get; private set; }

        public Rating(User student, Course course, decimal value)
        {
            RatingId = Guid.NewGuid();
            Student = student;
            Course = course;
            Value = value;
        }
    }
}
