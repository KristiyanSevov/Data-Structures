using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Judge : IJudge
{
    private SortedSet<int> users = new SortedSet<int>();
    private SortedSet<int> contests = new SortedSet<int>();
    private OrderedDictionary<int, Submission> submissions =
        new OrderedDictionary<int, Submission>();
    private Dictionary<SubmissionType, OrderedBag<Submission>> submissionsByType = 
        new Dictionary<SubmissionType, OrderedBag<Submission>>();
    private Dictionary<SubmissionType, HashSet<int>> contestsByType = //haven't handled delete here, no need for judge
       new Dictionary<SubmissionType, HashSet<int>>();
    private Dictionary<int, Dictionary<int, OrderedBag<Submission>>> submissionByUser = 
        new Dictionary<int, Dictionary<int, OrderedBag<Submission>>>();

    public void AddContest(int contestId)
    {
        contests.Add(contestId);
    }

    public void AddSubmission(Submission submission)
    { 
        if (!users.Contains(submission.UserId) || !contests.Contains(submission.ContestId))
        {
            throw new InvalidOperationException();
        }
        if (!submissions.ContainsKey(submission.Id))
        {
            submissions.Add(submission.Id, submission);
            if (!submissionsByType.ContainsKey(submission.Type))
            {
                submissionsByType.Add(submission.Type, new OrderedBag<Submission>((x, y) => x.Points.CompareTo(y.Points)));
            }
            submissionsByType[submission.Type].Add(submission);
            if (!contestsByType.ContainsKey(submission.Type))
            {
                contestsByType.Add(submission.Type, new HashSet<int>());
            }
            contestsByType[submission.Type].Add(submission.ContestId);
            if (!submissionByUser.ContainsKey(submission.UserId))
            {
                submissionByUser.Add(submission.UserId, new Dictionary<int, OrderedBag<Submission>>());
            }
            if (!submissionByUser[submission.UserId].ContainsKey(submission.ContestId))
            {
                submissionByUser[submission.UserId].Add(submission.ContestId, new OrderedBag<Submission>((x, y) => x.Points.CompareTo(y.Points)));
            }
            submissionByUser[submission.UserId][submission.ContestId].Add(submission);
        }
    }

    public void AddUser(int userId)
    {
        users.Add(userId);
    }

    public void DeleteSubmission(int submissionId)
    {
        if (!submissions.ContainsKey(submissionId))
        {
            throw new InvalidOperationException();
        }
        Submission sub = submissions[submissionId];
        submissionsByType[sub.Type].Remove(sub);
        submissionByUser[sub.UserId][sub.ContestId].Remove(sub);
        submissions.Remove(submissionId);
    }

    public IEnumerable<Submission> GetSubmissions()
    {
        foreach (var kvp in submissions)
        {
            yield return kvp.Value;
        }
    }

    public IEnumerable<int> GetUsers()
    {
        return users;
    }

    public IEnumerable<int> GetContests()
    {
        return contests;
    }

    public IEnumerable<Submission> SubmissionsWithPointsInRangeBySubmissionType(int minPoints, int maxPoints, SubmissionType submissionType)
    {
        return submissionsByType[submissionType]
            .Range(new Submission(0, minPoints, submissionType, 0, 0), true, new Submission(0, maxPoints, submissionType, 0, 0), true);
    }

    public IEnumerable<int> ContestsByUserIdOrderedByPointsDescThenBySubmissionId(int userId)
    {
        var contestsDict = submissionByUser[userId];
        var contests = contestsDict.OrderByDescending(x => x.Value.OrderByDescending(y => y.Points).ThenBy(z => z.Id).First().Points)
            .ThenBy(x => x.Value.OrderByDescending(y => y.Points).ThenBy(z => z.Id).First().Id);
        foreach (var contest in contests)
        {
            yield return contest.Key;
        }

    }

    public IEnumerable<Submission> SubmissionsInContestIdByUserIdWithPoints(int points, int contestId, int userId)
    {
        if (!submissionByUser.ContainsKey(userId) || !submissionByUser[userId].ContainsKey(contestId))
        {
            throw new InvalidOperationException();
        }
        var results = submissionByUser[userId][contestId]
            .Range(new Submission(0, points, SubmissionType.CSharpCode, 0, 0), true, new Submission(0, points, SubmissionType.CSharpCode, 0, 0), true);
        if (!results.Any())
        {
            throw new InvalidOperationException(); 
        }
        return results;
    }

    public IEnumerable<int> ContestsBySubmissionType(SubmissionType submissionType)
    {
        if (contestsByType.ContainsKey(submissionType))
        {
            return contestsByType[submissionType];
        }
        return Enumerable.Empty<int>();  
    }
}
