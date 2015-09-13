using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Array_Based_Stack.Tests
{
    [TestClass]
    public class UnitTestsArrayBasedStack
    {
        [TestMethod]
        public void PushAndPopElement_SouldWorkCorrectly()
        {
            var stack = new ArrayStack<int>();

            Assert.AreEqual(0, stack.Count);

            int number = 3;
            stack.Push(number);
            Assert.AreEqual(1, stack.Count);

            int numberFromStack = stack.Pop();
            Assert.AreEqual(number, numberFromStack);
            Assert.AreEqual(0, stack.Count);
        }

        [TestMethod]
        public void PushAndPop1000Elements_SouldWorkCorrectly()
        {
            int numberOfElements = 1000;
            var stack = new ArrayStack<string>();
            var elements = new string[numberOfElements];
            Assert.AreEqual(0, stack.Count);

            for (int i = 0; i < numberOfElements; i++)
            {
                var element = string.Format("{0}asfd{1}", i, i + 1);
                stack.Push(element);
                elements[i] = element;

                Assert.AreEqual(i + 1, stack.Count);
            }

            for (int i = numberOfElements - 1; i >= 0; i--)
            {
                var element = stack.Pop();

                Assert.AreEqual(elements[i], element);
                Assert.AreEqual(i, stack.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PopFromEmptyStack_ShouldThrowAnException()
        {
            var stack = new ArrayStack<string>();

            var element = stack.Pop();
        }

        [TestMethod]
        public void PushAndPopWithInitialCapacity1_SouldWorkCorrectly()
        {
            var stack = new ArrayStack<string>(1);

            Assert.AreEqual(0, stack.Count);
            
            var element1 = "element 1";
            stack.Push(element1);
            Assert.AreEqual(1, stack.Count);

            var element2 = "element 2";
            stack.Push(element2);
            Assert.AreEqual(2, stack.Count);

            var element1FromStack = stack.Pop();
            Assert.AreEqual(element2, element1FromStack);
            Assert.AreEqual(1, stack.Count);

            var element2FromStack = stack.Pop();
            Assert.AreEqual(element1, element2FromStack);
            Assert.AreEqual(0, stack.Count);
        }

        [TestMethod]
        public void ToArray_SouldWorkCorrectly()
        {
            var stack = new ArrayStack<int>();

            stack.Push(3);
            stack.Push(5);
            stack.Push(-2);
            stack.Push(7);

            var array = stack.ToArray();

            for (int i = 0; i < stack.Count; i++)
            {
                Assert.AreEqual(array[i], stack.Pop());
            }
        }

        [TestMethod]
        public void EmptyStackToArray_ArrayShouldBeEmpty()
        {
            var stack = new ArrayStack<int>();

            var array = stack.ToArray();

            Assert.AreEqual(0, array.Length);
        }
    }
}
