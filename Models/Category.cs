﻿public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public ICollection<Contact> Contacts { get; set; }
}
