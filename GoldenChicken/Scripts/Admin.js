var Media = new Vue({
    el: "#Gallery",
    data: {
        mediaType: 1, // 1)photo 2)videos
        items:[]
    },
    methods: {
        loadMediaItems: () => {
            axios.post('/Admin/MediaItems')
                .then(function (response) {
                    response.data.items.forEach(x => {
                        x.Path = "/Content/Media/" + (x.type === 0 ? "Photo" : x.type === 1 ? "Video" : "Fake") +"/"+x.Path;
                        Media.$data.items.push(x);
                    });
                })
                .catch(function (error) {
                    console.log(error);
                })
                .then(function () {
                    console.log("after");
                });
        }
    }
});
Media.loadMediaItems();