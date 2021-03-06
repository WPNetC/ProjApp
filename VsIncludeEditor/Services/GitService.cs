﻿using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace VsIncludeEditor.Services
{
    public class GitService
    {
        public static IEnumerable<string> GetBranchNames(FileInfo solution)
        {
            return GetBranchNames(solution?.DirectoryName);
        }

        public static IEnumerable<string> GetBranchNames(string path)
        {
            if (!Directory.Exists(path))
                yield break;

            var dInf = GetGitDirectory(path);

            if (dInf == null)
                yield break;

            using (var repo = new Repository(dInf.FullName))
            {
                var branches = repo.Branches;
                foreach (var branch in branches)
                {
                    yield return branch.FriendlyName;
                }
            }

        }

        public static IEnumerable<Branch> GetBranches(string path)
        {
            if (!Directory.Exists(path))
                yield break;

            var dInf = GetGitDirectory(path);

            if (dInf == null)
                yield break;

            using (var repo = new Repository(dInf.FullName))
            {
                var branches = repo.Branches;
                foreach (var branch in branches)
                {
                    yield return branch;
                }
            }

        }

        public static DirectoryInfo GetGitDirectory(string path)
        {
            var dInf = new DirectoryInfo(path);

            if (!Repository.IsValid(path))
            {
                if (!Path.GetDirectoryName(path).EndsWith(".git"))
                {
                    var pathGit = Path.Combine(path, ".git");
                    if (Directory.Exists(pathGit) && Repository.IsValid(pathGit))
                    {
                        dInf = new DirectoryInfo(Path.Combine(path, ".git"));
                    }
                    else
                    {
                        var parent = dInf.Parent;
                        dInf = null;
                        while (parent != null)
                        {
                            var folders = parent.GetDirectories(".git");
                            if (folders.Any())
                            {
                                dInf = folders.First();
                                break;
                            }
                            parent = parent.Parent;
                        }
                    }

                }
            }

            return dInf;
        }

        public static string GetCurrentBranchName(string path)
        {
            var dInf = GetGitDirectory(path);

            if (dInf == null)
                return "No valid repository found.";

            using (var repo = new Repository(dInf.FullName))
            {
                return repo.Head.FriendlyName;
            }
        }

        public static Commit[] GetCommits(string path)
        {
            var dInf = GetGitDirectory(path);

            if (dInf == null)
                return null;
            var repo = new Repository(dInf.FullName);
            try
            {
                return repo.Head.Commits.ToArray();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public static bool MoveCommitsToNewBranch(string path, string newBranchName, bool rollbackAll, int rollbackAmount = 0, bool deleteBranch = false)
        {
            var dInf = GetGitDirectory(path);

            if (dInf == null)
                return false;

            try
            {
                using (var repo = new Repository(dInf.FullName))
                {
                    var temp = repo.CreateBranch(newBranchName);

                    if (rollbackAll || rollbackAmount > 0)
                    {
                        var commits = repo.Head.Commits.ToArray();
                        Commit commit;
                        if (rollbackAll || rollbackAmount > commits.Length)
                        {
                            commit = commits.LastOrDefault();
                        }
                        else
                        {
                            commit = commits[rollbackAmount];
                        }
                        repo.Reset(ResetMode.Hard, commit);
                    }
                    else
                    {
                        repo.Reset(ResetMode.Hard);
                    }

                    if (!deleteBranch)
                        LibGit2Sharp.Commands.Checkout(repo, temp);
                    else repo.Branches.Remove(temp);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        public static bool CheckoutBranch(string path, Branch branch)
        {
            var dInf = GetGitDirectory(path);

            if (dInf != null)
            {
                var repo = new Repository(dInf.FullName);
                var repoDrive = dInf.FullName.Split(':')[0];
                try
                {
                    var pInf = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        UseShellExecute = false
                    };


                    using (var proc = new Process { StartInfo = pInf })
                    {
                        proc.Start();

                        proc.StandardInput.WriteLine($"{repoDrive}:");
                        proc.StandardInput.Flush();

                        proc.StandardInput.WriteLine($"cd {dInf.Parent.FullName}");
                        proc.StandardInput.Flush();

                        proc.StandardInput.WriteLine($"git checkout {branch.FriendlyName}");
                        proc.StandardInput.Flush();

                        proc.StandardInput.Close();
                        proc.WaitForExit();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    repo.Dispose();
                }
            }

            return false;
        }

        public static bool CheckoutCommit(string path, Commit commit)
        {
            var dInf = GetGitDirectory(path);

            if (dInf != null)
            {
                var repo = new Repository(dInf.FullName);
                try
                {
                    LibGit2Sharp.Commands.Checkout(repo, commit);
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            return false;
        }
    }
}
