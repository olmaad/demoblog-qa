import { User, Comment, Vote } from './model.js';
import { buildGetQuery } from "./api_util.js";

export const submitCommentAsync = async function(sessionKey, postId, text) {
    const response = await fetch("/api/comment", {
        headers: {
            "Content-Type": "application/json"
        },
        method: "POST",
        body: JSON.stringify({ sessionKey: sessionKey, postId: postId, text: text })
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