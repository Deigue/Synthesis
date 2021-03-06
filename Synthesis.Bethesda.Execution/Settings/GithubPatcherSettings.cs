using Noggog;
using Synthesis.Bethesda.Execution.Reporters;
using System;

namespace Synthesis.Bethesda.Execution.Settings
{
    public enum PatcherVersioningEnum
    {
        Tag,
        Branch,
        Commit,
    }

    public enum NugetVersioningEnum
    {
        Latest,
        Match,
        Manual,
    }

    public class GithubPatcherSettings : PatcherSettings
    {
        public string ID = string.Empty;
        public string RemoteRepoPath = string.Empty;
        public string SelectedProjectSubpath = string.Empty;
        public PatcherVersioningEnum PatcherVersioning = PatcherVersioningEnum.Branch;
        public string TargetTag = string.Empty;
        public string TargetCommit = string.Empty;
        public string TargetBranch = string.Empty;
        public bool LatestTag = true;
        public bool FollowDefaultBranch = true;
        public NugetVersioningEnum MutagenVersioning = NugetVersioningEnum.Latest;
        public string ManualMutagenVersion = string.Empty;
        public NugetVersioningEnum SynthesisVersioning = NugetVersioningEnum.Latest;
        public string ManualSynthesisVersion = string.Empty;

        public override void Print(IRunReporter logger)
        {
            logger.Write(default, $"[Git] {Nickname.Decorate(x => $"{x} => ")}{RemoteRepoPath}/{SelectedProjectSubpath} {PatcherVersioningString()}");
        }

        public string PatcherVersioningString()
        {
            switch (PatcherVersioning)
            {
                case PatcherVersioningEnum.Tag:
                    if (LatestTag)
                    {
                        return "Tag: Latest";
                    }
                    else
                    {
                        return $"Tag: {TargetTag}";
                    }
                case PatcherVersioningEnum.Branch:
                    if (FollowDefaultBranch)
                    {
                        return "Default Branch";
                    }
                    else
                    {
                        return $"Branch: {TargetBranch}";
                    }
                case PatcherVersioningEnum.Commit:
                    return $"Commit: {TargetCommit}";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
