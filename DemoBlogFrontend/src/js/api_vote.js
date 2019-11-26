export const postVoteAsync = async function(vote) {
    const response = await fetch("/api/vote", {
        headers: {
            "Content-Type": "application/json"
        },
        method: "POST",
        body: JSON.stringify(vote)
    });

    console.debug("Api: posted vote");

    return response.ok;
};

export const putVoteAsync = async function(vote) {
    const response = await fetch("/api/vote/" + vote.id, {
        headers: {
            "Content-Type": "application/json"
        },
        method: "PUT",
        body: JSON.stringify(vote)
    });

    console.debug("Api: vote put");

    return response.ok;
};

export const deleteVoteAsync = async function(vote) {
    const response = await fetch("/api/vote/" + vote.id, {
        method: "DELETE"
    });

    console.debug("Api: vote deleted");

    return response.ok;
};