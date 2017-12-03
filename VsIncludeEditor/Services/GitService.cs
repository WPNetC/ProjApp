using LibGit2Sharp;
using System.Collections.Generic;
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

        private static DirectoryInfo GetGitDirectory(string path)
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
    }
}
