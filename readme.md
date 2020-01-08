# How to install

## Using docker

> Note: initially you need root to run docker on linux (see https://docs.docker.com/install/linux/linux-postinstall/)

1. Clone git repository:

`git clone https://olmaad@bitbucket.org/olmaad/demoblog-qa.git`

2. Pull and build blog image:

`cd demoblog-qa/Docker/Run/Blog`

`docker-compose pull`

`docker-compose build`

This will download blog image and generate new test database. You can always generate new one running build again.

3. Start

`docker-compose up`

in background:

`docker-compose up -d`

to stop running in background:

`docker-compose stop`

Done! Open http://localhost:8080

# How to run tests

## Using docker

1. Assuming you already have repository cloned, pull tests image:

`cd demoblog-qa/Docker/Run/Tests`

`docker-compose pull`

2. Optional: set up test results directory

Set environment variable `TEST_RESULTS_VOLUME` regular way, or create `.env` file:

`TEST_RESULTS_VOLUME=<results_directory>`

3. Run tests

`docker-compose up --abort-on-container-exit`