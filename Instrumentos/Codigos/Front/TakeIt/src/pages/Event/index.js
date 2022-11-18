import { View, StyleSheet, Text, TouchableOpacity } from "react-native";
import * as Animatable from 'react-native-animatable'


export default function Event({route}) {
    const item = route.params;

    return (
        <View style={styles.container}>
            <View style={styles.imageContainer}>
                <Animatable.Image 
                    style={styles.image}
                    source={{uri: item.imageUrl}} 
                    animation="fadeIn"
                    delay={300}
                    resizeMode="contain"
                />
            </View>
            <View style={styles.textscontainer}>
                <Text style={styles.date}>{new Date(item.startDate).toLocaleDateString('pt-BR')} {'>'} {new Date(item.endDate).toLocaleDateString('pt-BR')}</Text>
                <Text style={styles.title}>{item.title}</Text>
                <Text style={styles.location}>{item.location}</Text>
                <Text style={styles.description}>{item.description}</Text>
            </View>
            <TouchableOpacity 
                style={styles.button}
                // onPress={ async () => {
                //     await login(username, password);
                //     navigation.navigate('Events');
                // } }
            >
                <Text style={styles.buttonText}>Comprar ingresso</Text>
            </TouchableOpacity>
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#ffff'
    },
    imageContainer: {
        top: '2%'
    },
    image: {
        height: 200,
        borderRadius: 15,
        border: 1,
        width: '90%',
        marginLeft: '5%',
    },
    textscontainer: {
        alignItems: 'center',
    },
    date: {
        
        fontSize: 20,
        // fontWeight: 'bold',
        color: '#7ED957',
        marginLeft: '6%',
        top: 20
    },
    title: {
        fontSize: 28,
        fontWeight: 'bold',
        marginLeft: '6%',
        top: 20
    },
    location: {
        fontSize: 20,
        marginLeft: '6%',
        top: 19
    },
    description: {
        fontSize: 20,
        top: 50
    },
    button: {
        backgroundColor: '#7ED957',
        borderRadius: 15,
        alignSelf: 'center',
        height: 50,
        width: 250,
        marginTop: '80%',
        alignItems: 'center',
        justifyContent: 'center',
        marginBottom: 15
    },
    buttonText: {
        fontSize: 18,
        color: '#fff',
        fontWeight: 'bold'
    },
});