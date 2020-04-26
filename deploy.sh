docker build -t UprootTools-image .

docker tag UprootTools-image registry.heroku.com/UprootTools/web


docker push registry.heroku.com/UprootTools/web

heroku container:release web -a UprootTools