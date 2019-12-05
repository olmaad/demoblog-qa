import { User, Post, Vote } from './model.js';
import { buildGetQuery } from "./api_util.js";

export const loadPostsAsync = async function(session, date) {
    let queryParams = {};

    if (session != null) {
        queryParams["userId"] = session.userId;
    }

    date = new Date();

    if (date != null) {
        queryParams["date"] = "" + date.getFullYear() + "-" + (date.getMonth() + 1).toString().padStart(2, "0") + "-" + date.getDate().toString().padStart(2, "0");
    }

    const query = await buildGetQuery(queryParams);

    const response = await fetch("/api/posts" + query);

    const json = await response.json();

    if (json == null || json.posts == null || json.users == null) {
        return null;
    }

    let posts = new Map();

    for (let it in json.posts) {
        const post = Post.fromJson(json.posts[it])

        posts.set(post.id, post);
    }

    let users = new Map();

    for (let it in json.users) {
        const user = User.fromJson(json.users[it]);

        users.set(user.id, user);
    }

    let votes = new Map();

    for (let it in json.votes) {
        const vote = Vote.fromJson(json.votes[it]);

        votes.set(vote.entityId, vote)
    }

    console.group("Api: loaded posts:");
    console.debug(posts);
    console.debug(users);
    console.debug(votes);
    console.groupEnd();

    return {
        posts: posts,
        users: users,
        votes: votes
    };
};

export const loadPostAsync = async function(id, session) {
    const query = await buildGetQuery((session == null) ? {} : { userId: session.userId });

    const response = await fetch("/api/posts/" + id + query);

    const json = await response.json();

    if (json == null || json.post == null || json.user == null) {
        return null;
    }

    const post = Post.fromJson(json.post);
    const user = User.fromJson(json.user);

    let vote = null;

    if (json.vote != null) {
        vote = Vote.fromJson(json.vote);
    }

    console.group("Api: loaded post:");
    console.debug(post);
    console.debug(user);
    console.debug(vote);
    console.groupEnd();

    return {
        post: post,
        user: user,
        vote: vote
    };
};

export const submitPostAsync = async function(post) {
    let method;
    let url;

    if (post.id >= 0) {
        method = "PUT";
        url = "/api/posts/" + post.id;
    }
    else {
        method = "POST";
        url = "/api/posts";
    }

    const response = await fetch(url, {
        headers: {
            "Content-Type": "application/json"
        },
        method: method,
        body: JSON.stringify(post)
    });

    const text = await response.text();
    const id = parseInt(text);

    console.group("Api: posted post:")
    console.debug("Id = " + id);
    console.groupEnd();

    return id;
};

export const removePostAsync = async function(id) {
    const response = await fetch("/api/posts/" + id, {
        method: "DELETE"
    });

    return response.ok;
};