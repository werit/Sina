####
# Necessary setups: 
# env_vars: 
# 	GITHUB_PACK_TOKEN
####

####
# Work with github package:
# 	Be sure to have nuget.config file
#	be sure to specify version of app in .csproj
# 	for more info read: https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-nuget-registry#publishing-a-package-using-a-nugetconfig-file
####

####
# Call like: make build_nuget
# Make sure to bump version in .csproj file
####
build_nuget:
	dotnet pack --configuration Release

####
# Call like: make push_nuget VERSION=0.0.2 GITHUB_PACK_TOKEN=get_githupackage__token_from_last_pass
####
push_nuget:
	dotnet nuget push "bin/Release/sina.messaging.contracts.$(VERSION).nupkg" --source "github" --api-key $(GITHUB_PACK_TOKEN)