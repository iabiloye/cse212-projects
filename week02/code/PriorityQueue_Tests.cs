using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add items with different priorities and dequeue them to verify highest priority comes first
    // Expected Result: Items should be dequeued in priority order (highest first), regardless of insertion order
    // Defect(s) Found: The Dequeue loop did not check the last item (< _queue.Count - 1 instead of < _queue.Count).  
    // while the chosen item was not removed after being dequeued, which left stale items in the queue.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        
        // Add items with different priorities (not in priority order)
        priorityQueue.Enqueue("Noun", 1);
        priorityQueue.Enqueue("Pronoun", 5);
        priorityQueue.Enqueue("Verb", 3);
        priorityQueue.Enqueue("Adverb", 10);
        
        // Should dequeue in priority order (highest first)
        Assert.AreEqual("Adverb", priorityQueue.Dequeue());
        Assert.AreEqual("Pronoun", priorityQueue.Dequeue());
        Assert.AreEqual("Verb", priorityQueue.Dequeue());
        Assert.AreEqual("Noun", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add items with same priority and verify FIFO behavior for equal priorities
    // Expected Result: When priorities are equal, first item added should be dequeued first
    // Defect(s) Found: Incorrect loop boundary and missing removal after dequeue caused wrong behavior.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        
        // Add items with same priority
        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Third", 5);
        
        // Maintaining first in first out order for equal priorities
        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Test dequeuing from empty queue
    // Expected Result: Should throw InvalidOperationException with appropriate message
    // Defect(s) Found: None found cause implementation correctly throws exception when queue is empty.
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();
        
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail($"Unexpected exception of type {e.GetType()} caught: {e.Message}");
        }
    }

    [TestMethod]
    // Scenario: Test with negative priorities
    // Expected Result: Higher numbers should still have higher priority (including negative numbers)
    // Defect(s) Found: A loop skipped last element and removal was missing, causing wrong order.
    public void TestPriorityQueue_NegativePriorities()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("Adjective", -10);
        priorityQueue.Enqueue("Noun", -5);
        priorityQueue.Enqueue("Verb", 0);
        priorityQueue.Enqueue("Adverb", 3);
        
        // Should dequeue in priority order (highest number first)
        Assert.AreEqual("Adverb", priorityQueue.Dequeue());
        Assert.AreEqual("Verb", priorityQueue.Dequeue());
        Assert.AreEqual("Noun", priorityQueue.Dequeue());
        Assert.AreEqual("Adjective", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Test single item queue
    // Expected Result: Single item should be dequeued successfully
    // Defect(s) Found: Confirmed that the “last item bug”  was fixed.
    public void TestPriorityQueue_SingleItem()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("OnlyItem", 42);
        
        Assert.AreEqual("OnlyItem", priorityQueue.Dequeue());
        
        // Verify queue is now empty
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown for empty queue.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    [TestMethod]
    // Scenario: Test mixed enqueue/dequeue operations
    // Expected Result: Should handle dynamic additions and removals correctly
    // Defect(s) Found: Failure in removing dequeued items broke consistency during mixed operations.
    public void TestPriorityQueue_MixedOperations()
    {
        var priorityQueue = new PriorityQueue();
        
        // Add initial items
        priorityQueue.Enqueue("Verb", 5);
        priorityQueue.Enqueue("Noun", 1);
        
        // Dequeue highest priority
        Assert.AreEqual("Verb", priorityQueue.Dequeue());
        
        // Add more items
        priorityQueue.Enqueue("Pronoun", 10);
        priorityQueue.Enqueue("Conjunction", 15);
        
        // Dequeue in priority order
        Assert.AreEqual("Conjunction", priorityQueue.Dequeue());
        Assert.AreEqual("Pronoun", priorityQueue.Dequeue());
        Assert.AreEqual("Noun", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Test with zero priority
    // Expected Result: Zero priority should be handled correctly in comparison with negative and positive
    // Defect(s) Found: This is missing removal after dequeue still affected correctness.
    public void TestPriorityQueue_ZeroPriority()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("NegativeNoun", -1);
        priorityQueue.Enqueue("ZeroPronoun", 0);
        priorityQueue.Enqueue("PositiveVerb", 1);
        
        Assert.AreEqual("PositiveVerb", priorityQueue.Dequeue());
        Assert.AreEqual("ZeroPronoun", priorityQueue.Dequeue());
        Assert.AreEqual("NegativeNoun", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Test ToString method works correctly
    // Expected Result: Should display queue contents in insertion order with priorities
    // Defect(s) Found: None ToString works, but earlier bugs in dequeue could indirectly affect what remains in queue.
    public void TestPriorityQueue_ToString()
    {
        var priorityQueue = new PriorityQueue();
        
        priorityQueue.Enqueue("First", 1);
        priorityQueue.Enqueue("Second", 2);
        
        string result = priorityQueue.ToString();
        
        // Should show items in insertion order
        Assert.IsTrue(result.Contains("First (Pri:1)"));
        Assert.IsTrue(result.Contains("Second (Pri:2)"));
    }
}
