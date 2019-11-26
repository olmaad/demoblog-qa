import { Session, User, Post, Comment, Vote, VoteType } from './model.js';

export * from "./api_vote.js";

const buildGetQuery = async function(params) {
    let query = Object.keys(params)
        .map(k => encodeURIComponent(k) + '=' + encodeURIComponent(params[k]))
        .join('&');

    console.group("Api: build get query:");
    console.debug("params:");
    console.debug(params);
    console.debug("query: " + query);
    console.groupEnd();

    if (query == "") {
        return "";
    }

    return ("?" + query);
};

export const loadPostsAsync = async function(session) {
    const query = await buildGetQuery((session == null) ? {} : { userId: session.userId });

    const response = await fetch("/api/posts" + query);

    const json = await response.json();

    if (json == null || json.posts == null || json.users == null) {
        return null;
    }

    let posts = [];

    for (let it in json.posts) {
        posts.push(Post.fromJson(json.posts[it]));
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

    return response.ok;
};

export const removePostAsync = async function(id) {
    const response = await fetch("/api/posts/" + id, {
        method: "DELETE"
    });

    return response.ok;
};

export const createSessionAsync = async function(login, password) {
    const response = await fetch("/api/session", {
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        },
        method: "POST",
        body: JSON.stringify({ login: login, password: password })
    });

    if (!response.ok) {
        return null;
    }

    const json = await response.json()

    const session = json.session == null ? null : Session.fromJson(json.session);
    const user = json.user == null ? null : User.fromJson(json.user);

    console.group("Api: created session:");
    console.debug(session);
    console.groupEnd();

    return { session: session, user: user };
};

export const loadSessionAsync = async function(id) {
    const response = await fetch("/api/session/" + id);

    if (!response.ok) {
        return null;
    }

    const json = await response.json();

    const session = json.session == null ? null : Session.fromJson(json.session);
    const user = json.user == null ? null : User.fromJson(json.user);

    console.group("Api: loaded session:");
    console.debug(session);
    console.groupEnd();

    return { session: session, user: user };
};

export const removeSessionAsync = async function(id) {
    const response = await fetch("/api/session/" + id, {
        method: "DELETE"
    });

    return response.ok;
};

export const registerUserAsync = async function(login, name, password) {
    const response = await fetch("/api/user", {
        headers: {
            "Content-Type": "application/json"
        },
        method: "POST",
        body: JSON.stringify({
            login: login,
            name: name,
            password: password
        })
    });

    if (response.ok) {
        console.debug("Api: created user");
    }

    return response.ok;
}

export const loadUserAsync = async function(id) {
    const response = await fetch("/api/user/" + id);

    if (!response.ok) {
        return null;
    }

    const user = await response.json();

    return user;
};

export const submitCommentAsync = async function(comment) {
    const response = await fetch("/api/comment", {
        headers: {
            "Content-Type": "application/json"
        },
        method: "POST",
        body: JSON.stringify(comment)
    });

    if (!response.ok) {
        return null;
    }

    const id = Number(await response.text());

    console.group("Api: posted comment:");
    console.debug("Id = " + id);
    console.groupEnd();

    return id;
};

export const loadCommentsAsync = async function(postId, session) {
    const query = await buildGetQuery((session == null) ? {} : { userId: session.userId});

    const response = await fetch("/api/comment/" + postId + query);

    if (!response.ok) {
        return null;
    }

    const json = await response.json();

    if (json == null || json.comments == null || json.users == null) {
        return null;
    }

    let comments = [];

    for (let it in json.comments) {
        comments.push(Comment.fromJson(json.comments[it]));
    }

    let users = new Map();

    for (let it in json.users) {
        const user = User.fromJson(json.users[it]);

        users.set(user.id, user);
    }

    let votes = new Map();

    for (let it in json.votes) {
        const vote = Vote.fromJson(json.votes[it]);

        votes.set(vote.entityId, vote);
    }

    console.group("Api: loaded comments:");
    console.debug(comments);
    console.debug(users);
    console.debug(votes);
    console.groupEnd();

    return { comments: comments, users: users, votes: votes };
};
