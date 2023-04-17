#TODO: split into smaller parts and start processes with helm 
start-all:
	cd ../recipies_ms/scripts/ ; docker-compose --env-file ./.env -f ./docker-compose-db.yaml -p sina up -d
	
stop-all:
	cd ../recipies_ms/scripts/ ; docker-compose --env-file ./.env -f ./docker-compose-db.yaml -p sina down
	
helm-recipe-up:
	cd ../recipies_ms/chart ; helm install sina-recipe-app -n sina --create-namespace sina-recipe-service\ 

helm-recipe-upg:
	cd ../recipies_ms/chart ; helm upgrade sina-recipe-app -n sina sina-recipe-service\
	
helm-recipe-down:
	cd ../recipies_ms/chart ; helm uninstall -n sina sina-recipe-app