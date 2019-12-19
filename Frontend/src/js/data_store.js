import { get, writable } from 'svelte/store';

import { loadPostAsync, loadCommentsAsync } from "./api.js";
import { Vote, VoteType } from "./model.js";

export const user = writable(null);
export const session = writable(null);

export const date = writable(new Date());

export let users = writable(new Map());
export let posts = writable(new Map());
export let postVotes = writable(new Map());

// Viewer
export let storeViewerPostId = writable(-1);
export let storeViewerPost = writable(null);
export let storeViewerUser = writable(null);
export let storeViewerVote = writable(null);
export let storeViewerCommentList = writable([]);
export let storeViewerCommentUsers = writable(new Map());
export let storeViewerCommentVotes = writable(new Map());

export const consumeSessionBundle = async function(bundle) {
    user.set(bundle.user);
    session.set(bundle.session);
};

export const consumePostsBundle = async function(bundle) {
    users.update(u => new Map([...u, ...bundle.users]));
    postVotes.update(v => new Map([...v, ...bundle.votes]));

    posts.set(bundle.posts);
};

export const handleVoteUpdate = async function(vote) {
    switch(vote.type) {
        case VoteType.post: {
            postVotes.update(function(m) {
                m.set(vote.entityId, vote);
                return m;
            });

            break;
        }
        case VoteType.comment: {
            storeViewerCommentVotes.update(function(m) {
                m.set(vote.entityId, vote);
                return m;
            });

            break;
        }
    }
};

export const getPostBundle = async function(id) {
    const postMap = get(posts);
    const userMap = get(users);
    const postVoteMap = get(postVotes);

    if (postMap.has(id)) {
        let bundle = {
            post: postMap.get(id),
            user: null,
            vote: null
        };

        bundle.user = userMap.get(bundle.post.userId);
        
        if (postVoteMap.has(id)) {
            bundle.vote = postVoteMap.get(id);
        }

        console.groupCollapsed("DataStore: found post bundle:");
        console.debug(bundle);
        console.groupEnd();

        return bundle;
    }

    const bundle = await loadPostAsync(id, get(session));

    return bundle;
};

export const getCommentBundle = async function(postId) {
    const bundle = await loadCommentsAsync(postId, get(session));

    return bundle;
};

storeViewerPostId.subscribe(async function(value) {
    console.debug("DataStore: postId changed: " + value);

    if (value < 0) {
        return;
    }

    const postBundle = await getPostBundle(value);

    storeViewerPost.set(postBundle.post);
    storeViewerUser.set(postBundle.user);

    if (postBundle.vote == null) {
        console.debug("DataStore: vote not found, created empty");
        postBundle.vote = Vote.create(VoteType.post, postBundle.post.userId, value, 0);
    }

    storeViewerVote.set(postBundle.vote);

    const commentsBundle = await getCommentBundle(value);

    storeViewerCommentList.set(commentsBundle.comments);
    storeViewerCommentUsers.set(commentsBundle.users);
    storeViewerCommentVotes.set(commentsBundle.votes);
});