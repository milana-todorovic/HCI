// File:    BlogPost.cs
// Author:  Gudli
// Created: 20 April 2020 11:54:01
// Purpose: Definition of Class BlogPost

using System;

namespace Model.Blog
{
    public class BlogPost : Repository.Generics.Entity<int>
    {
        private String title;
        private String text;
        private Object picture;
        private DateTime timeStamp;
        private int id;

        private BlogAuthor blogAuthor;

        public string Title { get => title; set => title = value; }
        public string Text { get => text; set => text = value; }
        public object Picture { get => picture; set => picture = value; }
        public DateTime TimeStamp { get => timeStamp; set => timeStamp = value; }
        public BlogAuthor BlogAuthor { get => blogAuthor; set => blogAuthor = value; }

        public int GetKey()
        {
            throw new NotImplementedException();
        }

        public void SetKey(int id)
        {
            throw new NotImplementedException();
        }
    }
}