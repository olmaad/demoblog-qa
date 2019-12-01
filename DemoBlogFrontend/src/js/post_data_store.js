import { get, writable } from 'svelte/store';

import { user } from "./data_store.js";

export let commentList = writable([]);
export let commentUsers = writable(new Map());
export let commentVotes = writable(new Map());

export const consumeCommentBundle = async function(bundle) {
    commentList.set(bundle.comments);
    commentUsers.set(bundle.users);
    commentVotes.set(bundle.votes);
};

export const addComment = async function(comment) {
    const userValue = get(user);

    if (!get(commentUsers).has(userValue.id)) {
        commentUsers.update(v => v.set(userValue.id, userValue));
    }

    commentList.update(v => v = [ ...v, comment ]);
};