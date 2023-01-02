####
# Necessary setups: 
# env_vars: 
# 	GITHUB_PACK_TOKEN (value can be found in last_pass)
####

version=0.0.1

run:
	cd ./scripts/ && docker-compose --env-file ./.env -f ./docker-compose-db.yaml -p sina up -d

####
# Call like: make build_image version=0.0.1
####	
build_image:
	docker build --build-arg github_token=${GITHUB_PACK_TOKEN} -t recipes:$(version) .