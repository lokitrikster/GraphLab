var network, nodesDS, edgesDS;

function bindGraph(nodes, edges) {
    var container = document.getElementById("graph");
    nodesDS = new vis.DataSet(nodes);
    edgesDS = new vis.DataSet(edges);
    var data = {
        nodes: nodesDS,
        edges: edgesDS
    };
    var options = {
        edges: {
            width: 5,
            physics: false
        },
        physics: {
            solver: "repulsion"
        }
    };
    network = new vis.Network(container, data, options);
}

function updateVertex(vertex) {
    nodesDS.update(vertex);
}

function updateEdge(edge) {
    edgesDS.update(edge);
}