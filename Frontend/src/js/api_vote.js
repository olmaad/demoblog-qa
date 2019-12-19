import { buildGetQuery } from "./api_util.js";

export const postVoteAsync = async function(sessionKey, vote) {
    const response = await fetch("/api/vote", {
        headers: {
            "Content-Type": "application/json"
        },
        method: "POST",
        body: JSON.stringify({ sessionKey: sessionKey, vote: vote })
    });

    const text = await response.text();
    const id = parseInt(text);

    console.groupCollapsed("Api: posted vote");
    console.debug("Id = " + id);
    console.groupEnd();

    return id;
};

export const putVoteAsync = async function(sessionKey, vote) {
    const response = await fetch("/api/vote/" + vote.id, {
        headers: {
            "Content-Type": "application/json"
        },
        method: "PUT",
        body: JSON.stringify({ sessionKey: sessionKey, vote: vote })
    });

    console.debug("Api: vote put");

    return response.ok;
};

export const deleteVoteAsync = async function(sessionKey, vote) {
    const query = await buildGetQuery({ sessionKey: sessionKey });

    const response = await fetch("/api/vote/" + vote.id + query, {
        method: "DELETE"
    });

    console.debug("Api: vote deleted");

    return response.ok;
};