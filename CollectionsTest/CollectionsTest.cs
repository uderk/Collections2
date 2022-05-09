using Collections;
using NUnit.Framework;
using System;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
       //please add your tests about Collection.cs here 
        [OneTimeSetUp]
        public void SetUp()
        {
            Console.WriteLine("Test Started" +DateTime.Now);
        }
        [Test]
        [Category("Critical")]
        public void Test_Int_Items()
        {
            // this works only for INT items 

            //this is arrange section

            Collection<int> nums = new Collection<int>();
            int numberOfElements = 10;
            //Act
            for (int i = 0; i < numberOfElements; i++)
            {
                nums.Add(i);
            }

           // int result = nums[0];
            //Assert
          //  Assert.AreEqual(3, nums.Count);
            for (int i = 0; i < numberOfElements; i++)
            {
                Does.Contain(i);
            }
        } 

        [Test]
        [Category("Critical")]
        public void Test_AddRange()
        {
            //arange 
            Collection<int> nums = new Collection<int>();
            //act
            nums = new Collection<int>(new int[] { 10, 20, 30,45,55 });
            int counted = nums.Count;
            //Assert 
            Assert.AreEqual(5, nums.Count, "Range is not added");
        }  

        [Test]
        public void Test_AddRangeWithGrow()
        {
            //Arrange
            Collection<int> nums = new Collection<int>();
            int oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();
            //Act
            nums.AddRange(newNums);
            string expectedNums="[" +string.Join(", ",newNums)+"]";
            //Assert
            Assert.AreEqual(nums.ToString(), expectedNums.ToString());

        }

        [Test]
        public void Test_InsertAt_Shoud_Throw_Exeption()
        {
           
            //Arange
            Collection<int> nums = new Collection<int>();
           
            //Act and Assert
            Assert.Throws<System.ArgumentOutOfRangeException>(() => nums.InsertAt(10, 15));
        }
       
        [Test]
        public void Test_InsertAt_ShouldWork()
        { 
            //Arrage
            Collection<int> nums = new Collection<int>(20);

            // nums = new Collection<int>(new int[] { 10, 20, 30, 45, 55 });
            //  nums.InsertAt(2, 15);

            for (int i = 0; i < 20; i++)
            {
                nums.Add(0);
            }

            nums.InsertAt(10, 15);

            Assert.AreEqual(15, nums[10]);
        }  

        [Test] 
        public void Test_Exchange()
        {
            // Arrange 
            Collection<int> nums = new Collection<int>();
            nums = new Collection<int>(new int[] { 10, 20, 30, 45, 55 });
            //Act 
            nums.Exchange(0, 1);
            //Assert 
            Assert.AreEqual(20, nums[0]);
            Assert.AreEqual(10, nums[1]);
        }

        [Test]
        public void Test_RemoveAt()
        {
            //In my opinion the RemoveAt() does not work correctly.It realy deletes the elements(see the first Assert), but it also
            //shifted all elements to left. I want the nums(i) to be empty or [] after remove
            //no another element
            // Arrange 
            Collection<int> nums = new Collection<int>();
            nums = new Collection<int>(new int[] { 10, 20, 30, 45, 55 });
            //Act 
            nums.RemoveAt(1);
            //Assert
            Assert.AreNotEqual(20, nums[1]);
            Assert.AreEqual("[]", nums[1]);
        }

        [Test]
        public void Test_Clear()
        {
            //Arrange
            Collection<int> nums = new Collection<int>();
            nums = new Collection<int>(new int[] { 10, 20, 30, 45, 55 });
            //Act
            nums.Clear();
            //Arrange
            Assert.That(nums.ToString(),Is.EqualTo("[]")); //test passes this way, but way is nit working with .IsEmpty?
        }

        [Test]
        public void Test_EmptyContructor()
        {
            Collection<int> nums = new Collection<int>();
            //Assert
            Assert.AreEqual(nums.ToString(), "[]");
        }

        [Test]
        public void Test_Constructur_OneIntElement()
        {
            //Arrange
            Collection<int> nums = new Collection<int>(10);
            //Assert
            Assert.That(nums[0], Is.EqualTo(10));
            Assert.AreEqual(1, nums.Count);
        }

        [Test]
        public void Test_Constructor_MultipleElements()
        {
            Collection<int> nums = new Collection<int>();
            nums = new Collection<int>(new int[] { 1,2,3,4,5 });
            //multiple Assert in for loop
            for (int i = 0; i < nums.Count; i++)
            {
                Assert.AreEqual(i+1, nums[i]);
            }
            Assert.AreEqual(5, nums.Count);
        }  

        [Test]
        public void Test_Constructor_String_Elements()
        {
            Collection<string> names = new Collection<string>("Veni", "Deni", "Mischa");
            //Assertioins
            Assert.That(3, Is.EqualTo(names.Count));
            Assert.AreEqual("Veni", names[0]);
            Assert.AreEqual("Deni", names[1]);
            Assert.AreEqual("Mischa", names[2]);
        } 

        [Test]
        public void Test_MixedTypeCollections()
        {
            //Arrange
            Collection<string> names = new Collection<string>("Veni", "Deni", "Mischa");
            Collection<int> nums = new Collection<int>(new int[] {1, 2, 3});
            Collection<DateTime> dates = new Collection<DateTime>(DateTime.Today);
            //Act
            var mixedTypeCollection = new Collection<object>(names, nums, dates);
            //Assert
            Assert.That(mixedTypeCollection.ToString(),Is.EqualTo($"[[Veni, Deni, Mischa], [1, 2, 3], [{DateTime.Today.ToString()}]]"));
        }

        [Test, MaxTime(1000000)] //This test fails on the last assert, must be checked 
        //MaxTime is in milisec
        public void Test_Collection_OneMilionObjects()
        {
            //Arrange
            const int objectsNumber = 1000000;
            Collection<int> nums = new Collection<int>(new int[] { 1, 2, 3 });
            //Act
            nums.AddRange(Enumerable.Range(1, objectsNumber).ToArray());
            //Assert
            Assert.That(nums.Capacity > nums.Count);
            for (int i = objectsNumber-1; i >=0; i--)
            {
                nums.RemoveAt(i);
            }
            Assert.That(nums.ToString(), Is.EqualTo("[]")); 
        }
        
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Console.WriteLine("Test Deleted" + DateTime.Now);
        }
    }
}