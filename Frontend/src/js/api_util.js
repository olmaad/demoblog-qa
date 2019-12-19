export const buildGetQuery = async function(params) {
    let query = Object.keys(params)
        .map(k => encodeURIComponent(k) + '=' + encodeURIComponent(params[k]))
        .join('&');

    console.groupCollapsed("Api: build get query:");
    console.debug("params:");
    console.debug(params);
    console.debug("query: " + query);
    console.groupEnd();

    if (query == "") {
        return "";
    }

    return ("?" + query);
};