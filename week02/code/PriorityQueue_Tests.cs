using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue several words with different priorities, then dequeue.
    // Expected Result: Dequeue returns the word with the highest priority.
    // Defect(s) Found: 
    public void TestPriorityQueue_HighestPriorityWord()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("noun", 1);
        priorityQueue.Enqueue("verb", 3);
        priorityQueue.Enqueue("adverb", 2);
        Assert.AreEqual("verb", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue multiple words with the same highest priority.
    // Expected Result: Dequeue returns the first word with the highest priority (FIFO).
    // Defect(s) Found: 
    public void TestPriorityQueue_FIFOSamePriorityWord()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("pronoun", 5);
        priorityQueue.Enqueue("adverb", 5);
        priorityQueue.Enqueue("noun", 3);
        Assert.AreEqual("pronoun", priorityQueue.Dequeue());
        Assert.AreEqual("adverb", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue from an empty queue.
    // Expected Result: Throws InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();
        var ex = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", ex.Message);
    }

    
}