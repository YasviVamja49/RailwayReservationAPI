export class TrainModel {
    trainNo!:number;
    trainName: string;
    startloc: string;
    endloc: string;
    arrivaltime: string;
    departuretime: string;
    arrivaldate: string;
    ac1tier: number;
    ac2tier: number;
    ac3tier: number;
    sleeper: number;
    tatkal: number;
    ladies: number;
    baseFare:number;

    constructor(
        trainName: string,
        startloc: string,
        endloc: string,
        arrivaltime: string,
        departuretime: string,
        arrivaldate: string,
        ac1tier: number,
        ac2tier: number,
        ac3tier: number,
        sleeper: number,
        tatkal: number,
        ladies: number,
        baseFare:number
    ) {
        
        this.trainName = trainName;
        this.startloc = startloc;
        this.endloc = endloc;
        this.arrivaltime = arrivaltime;
        this.departuretime = departuretime;
        this.arrivaldate = arrivaldate;
        this.ac1tier = ac1tier;
        this.ac2tier = ac2tier;
        this.ac3tier = ac3tier;
        this.sleeper = sleeper;
        this.tatkal = tatkal;
        this.ladies = ladies;
        this.baseFare=baseFare;
    }
}
