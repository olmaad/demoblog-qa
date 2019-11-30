import { get, writable, readable } from 'svelte/store';

import { loadPostAsync } from "./api.js";

export const routePostId = writable(-1);

export const post = readable(null);

export const user = writable(null);
export const session = writable(null);

export let users = writable(new Map());
export let posts = writable(new Map());
export let postVotes = writable(new Map());

export const consumeSessionBundle = async function(bundle) {
    user.set(bundle.user);
    session.set(bundle.session);
};

export const consumePostsBundle = async function(bundle) {
    users.update(u => new Map([...u, ...bundle.users]));
    postVotes.update(v => new Map([...v, ...bundle.votes]));

    posts.set(bundle.posts);
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

        console.group("DataStore: found post bundle:");
        console.debug(bundle);
        console.groupEnd();

        return bundle;
    }

    const bundle = await loadPostAsync(id, session);

    return bundle;
};