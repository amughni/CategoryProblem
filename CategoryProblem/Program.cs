using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryProblem
{
    class Program
    {
        List<Category> categories = new List<Category>();
        
        static void Main(string[] args)
        {
            try
            {
                Program prg = new Program();
                prg.LoadTestData();

                RunTest1(prg);
                RunTest2(prg);
                Console.Read();
            }
            catch (Exception ee)
            {
                Console.Write("Error: " + ee.Message);
            }
        }

        private static void RunTest2(Program prg)
        {
            List<Category> myCategories = new List<Category>();
            myCategories = prg.getCategories(2);
            PrintResultsForTest2(myCategories);
            myCategories = prg.getCategories(3);
            PrintResultsForTest2(myCategories);
            Console.WriteLine();
            Console.WriteLine("Test 2 completed sucessfully");
        }

        private static void PrintResultsForTest2(List<Category> myCategories)
        {
            for (int i = 0; i < myCategories.Count; i++)
            {
                if(i+1 == myCategories.Count)
                    Console.Write(myCategories[i].Id);
                else
                    Console.Write(myCategories[i].Id + ", ");
            }
            Console.WriteLine();
        }

        private static void RunTest1(Program prg)
        {
            Category myCategory = prg.getCategory(201, true);
            Console.WriteLine("ParentCategoryID=" + myCategory.ParentId + ", Name=" + myCategory.Name + ", Keywords=" + myCategory.Keyword);
            myCategory = prg.getCategory(202, true);
            Console.WriteLine("ParentCategoryID=" + myCategory.ParentId + ", Name=" + myCategory.Name + ", Keywords=" + myCategory.Keyword);
            Console.WriteLine();
            Console.WriteLine("Test 1 completed sucessfully");
            Console.WriteLine();
        }

        protected void LoadTestData()
        {
            AddCategory(100, "Business", "Money", -1);
            AddCategory(200, "Tutoring", "Teaching", -1);
            AddCategory(101, "Accounting", "Taxes", 100);
            AddCategory(102, "Taxation", "", 100);
            AddCategory(201, "Computer", "", 200);
            AddCategory(103, "Corporate Tax", "", 101);
            AddCategory(202, "Operating System", "", 201);
            AddCategory(109, "Small Business Tax", "", 101);
        }

        protected Category getCategory(int id, bool alwaysKeyword)
        {
            Category myCategory = getCategory(id);
            if (alwaysKeyword && String.IsNullOrEmpty(myCategory.Keyword))
            {
                myCategory.Keyword = getCategory(myCategory.ParentId, alwaysKeyword).Keyword;
            }
            
            return myCategory;
        }

        protected Category getCategory(int id) 
        {
            foreach(Category category in categories)
            {
                if (category.Id == id)
                    return category;
            }
            return null;
        }

        protected List<Category> getCategories(int level) 
        {
            List<Category> myCategories = new List<Category>();
            foreach (Category category in categories)
            {
                if (category.Level == level)
                    myCategories.Add(category);
            }
            return myCategories;
        }

        protected int getLevel(int parentID, int level)
        {
            level = level + 1;
            int parentParentID = getCategory(parentID).ParentId;
            if (parentParentID > -1)
            {
                return getLevel(parentParentID, level);
            }
            return level;
        }
        
        protected void AddCategory(int id, string name, string value, int parentID)
        {
            Category category = new Category();
            category.Id = id;
            category.Name = name;
            category.Keyword = value;
            category.ParentId = parentID;
            setLevel(parentID, category);
            categories.Add(category);
        }

        private void setLevel(int parentID, Category category)
        {
            if (parentID == -1)
                category.Level = 1;
            else 
                category.Level = getLevel(parentID, 1);
        }

        protected class Category
        {
            public int Id { get; set; }

            public int ParentId { get; set; }

            public String Name { get; set; }

            public String Keyword{ get; set; }

            public int Level { get; set; }
        }
    }
}
