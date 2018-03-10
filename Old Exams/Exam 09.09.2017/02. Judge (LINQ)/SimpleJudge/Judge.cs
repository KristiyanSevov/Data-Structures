using System;
using System.Collections.Generic;
using System.Linq;

public class Judge : IJudge
{
    private HashSet<int> contests = new HashSet<int>();
    private HashSet<int> users = new HashSet<int>();
    private Dictionary<int, Submission> submissions = new Dictionary<int, Submission>();

    public void AddContest(int contestId)
    {
        contests.Add(contestId);
    }

    public void AddSubmission(Submission submission)
    {
        if (!(contests.Contains(submission.ContestId) && users.Contains(submission.UserId)))
        {
            throw new InvalidOperationException();
        }
        if (!submissions.ContainsKey(submission.Id))
        {
            submissions.Add(submission.Id, submission);
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
        submissions.Remove(submissionId);
    }

    public IEnumerable<Submission> GetSubmissions()
    {
        return submissions.Values.OrderBy(x => x);
    }

    public IEnumerable<int> GetUsers()
    {
        return users.OrderBy(x => x);
    }

    public IEnumerable<int> GetContests()
    {
        return contests.OrderBy(x => x);
    }

    public IEnumerable<Submission> SubmissionsWithPointsInRangeBySubmissionType(int minPoints, int maxPoints, SubmissionType submissionType)
    {
        return submissions.Values
            .Where(x => x.Type == submissionType && x.Points >= minPoints && x.Points <= maxPoints);
    }

    public IEnumerable<int> ContestsByUserIdOrderedByPointsDescThenBySubmissionId(int userId)
    {
        return submissions.Values
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Points)
            .ThenBy(x => x.Id)
            .Select(x => x.ContestId)
            .Distinct();
    }

    public IEnumerable<Submission> SubmissionsInContestIdByUserIdWithPoints(int points, int contestId, int userId)
    {
        var result = submissions.Values
            .Where(x => x.Points == points && x.UserId == userId && x.ContestId == contestId);
        if (result.Any())
        {
            return result;
        }
        throw new InvalidOperationException();
    }

    public IEnumerable<int> ContestsBySubmissionType(SubmissionType submissionType)
    {
        return submissions.Values
            .Where(x => x.Type == submissionType)
            .Select(x => x.ContestId)
            .Distinct();
    }
}
