import { get, writable } from 'svelte/store';

import * as MainStore from "./data_store.js";

export let postId = writable(-1);

export let post = writable(null);
export let user = writable(null);
export let vote = writable(null);

export let commentList = writable([]);
export let commentUsers = writable(new Map());
export let commentVotes = writable(new Map());

export const handleVoteUpdate = async function(vote) {
    commentVotes.update(function(m) {
        m.set(vote.entityId, vote);
        return m;
    });
};

export const addComment = async function(comment) {
    const userValue = get(MainStore.user);

    if (!get(commentUsers).has(userValue.id)) {
        commentUsers.update(v => v.set(userValue.id, userValue));
    }

    commentList.update(v => v = [ ...v, comment ]);
};

postId.subscribe(async function(value) {
    console.debug("ViewerDataStore: postId changed: " + value);

    if (value < 0) {
        return;
    }

    const postBundle = await MainStore.getPostBundle(value);

    post.set(postBundle.post);
    user.set(postBundle.user);
    vote.set(postBundle.vote);

    const commentsBundle = await MainStore.getCommentBundle(value);

    commentList.set(commentsBundle.comments);
    commentUsers.set(commentsBundle.users);
    commentVotes.set(commentsBundle.votes);
});

MainStore.postVotes.subscribe(async function(value) {
    console.debug("ViewerDataStore: post votes changed");

    if (value.has(postId)) {
        vote.set(value.get(postId));
    }
});