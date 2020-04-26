docker build -t uproot-tools-image .

docker tag uproot-tools-image registry.heroku.com/uproot-tools/web


docker push registry.heroku.com/uproot-tools/web

heroku container:release web -a uproot-tools