export default {
    getData: () => {
        return fetch('http://localhost:1311/api/news', {
            method: 'get'
        }).then(function (response) {
            return response.json();
        }).then(function (response) {
            return response;
        });
    }
}