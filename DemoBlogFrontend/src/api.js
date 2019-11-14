export const loadPostsAsync = async function() {
    const response = await fetch("/api/posts");
    const posts = await response.json();

    return posts;
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

    const session = await response.json();

    console.debug("Created session:");
    console.debug(session);

    return session;
};

export const loadSessionAsync = async function(id) {
    const response = await fetch("/api/session/" + id);

    if (!response.ok) {
        return null;
    }

    const session = await response.json();

    return session;
}

export const removeSessionAsync = async function(id) {
    const response = await fetch("/api/session/" + id, {
        method: "DELETE"
    });

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