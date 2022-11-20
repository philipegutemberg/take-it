import { useNavigation } from "@react-navigation/native";
import { ScrollView, StyleSheet, View, TouchableOpacity, Text, Image } from "react-native";
import { useContext, useEffect, useState } from "react";
import * as Animatable from 'react-native-animatable'
import axios from "axios";
import { LoadingContext } from "../../context/LoadingContext";
import Loading from "../../components/loading";
import {decode as atob, encode as btoa} from 'base-64'

export default function Ticket({route}) {
    const navigation = useNavigation();
    const [data, setData] = useState('');
    const {setIsLoading} = useContext(LoadingContext);
    const item = route.params;

    const getQrCode = async () => {
        try {
            setIsLoading(true);
            let responseImage = await axios.get("/api/v1/ticketvalidation/image/" + item.ticketCode, { responseType: 'arraybuffer' });

            setData(imgToBase64(responseImage.data));
        } catch (err) {
            console.error(err);
        } finally {
            setIsLoading(false);
        }
    };

    useEffect(() => {
        getQrCode();
    }, []);

    return (
        <View style={styles.container}>
            <View style={styles.eventContainer}>
                <Loading />
                <Animatable.Image
                    style={styles.image} 
                    source={{uri: item.imageUrl}} 
                    animation="fadeIn"
                    delay={200}
                    resizeMode="contain"
                />
                <Text style={styles.date}>{new Date(item.startDate).toLocaleDateString('pt-BR')} {'>'} {new Date(item.endDate).toLocaleDateString('pt-BR')}</Text>
                <Text style={styles.title}>{item.title}</Text>
                <Text style={styles.qualification}>{item.ticketName} ({item.qualification})</Text>
                <Text style={styles.ticketType}>R${item.priceBrl}</Text>
            </View>
            <View style={styles.eventContainer}>
                <Animatable.Image
                        style={styles.qrCodeImage} 
                        // source={{uri: item.imageUrl}} 
                        source={{uri: data}} 
                        animation="fadeIn"
                        delay={500}
                        resizeMode="contain"
                />
            </View>
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#ffff'
    },
    eventContainer: {
        flex: 1,
        top: '2%',
        marginBottom: 30
    },
    qrCodeContainer: {
        flex: 1,
        // top: '2%'
    },
    image: {
        height: 200,
        borderRadius: 15,
        border: 1,
        width: '90%',
        marginLeft: '5%',
    },
    qrCodeImage: {
        height: 250,
        width: 250,
        border: 3,
        width: '100%',
        // marginLeft: '5%',
    },
    textscontainer: {
        alignItems: 'center',
    },
    date: {
        fontSize: 20,
        // fontWeight: 'bold',
        color: '#7ED957',
        marginLeft: '6%',
        top: 3
    },
    title: {
        fontSize: 25,
        fontWeight: 'bold',
        marginLeft: '6%',
        top: 0
    },
    ticketType: {
        fontSize: 15,
        marginLeft: '6%',
        top: 0
    },
    qualification: {
        fontSize: 20,
        marginLeft: '6%',
        top: 0
    },
    buttonArea: {
        flex: 1,
    },
    button: {
        backgroundColor: '#7ED957',
        borderRadius: 15,
        alignSelf: 'center',
        height: 50,
        width: 250,
        bottom: 0,
        alignItems: 'center',
        justifyContent: 'center'
    },
    buttonText: {
        fontSize: 18,
        color: '#fff',
        fontWeight: 'bold'
    },
});

const imgToBase64 = (img) => {
    let image = btoa(new Uint8Array(img)
                .reduce((data, byte) => data + String.fromCharCode(byte), ''));

    return `data:${responseImage.headers['content-type'].toLowerCase()};base64,${image}`;
};